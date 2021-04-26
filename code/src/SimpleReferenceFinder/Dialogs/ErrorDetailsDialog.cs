/*
 * MIT License
 * 
 * Copyright (c) 2021 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows.Forms;

namespace Plexdata.SimpleReferenceFinder.Dialogs
{
    public partial class ErrorDetailsDialog : Form
    {
        private const String fakeNode = ".:fake-node:.";
        private readonly Exception exception = null;

        public ErrorDetailsDialog()
            : base()
        {
            this.InitializeComponent();
        }

        public ErrorDetailsDialog(Exception exception)
            : this()
        {
            this.exception = exception;
        }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);
            this.InitializeControls();
        }

        private void InitializeControls()
        {
            this.trvDetails.Nodes.Clear();
            this.txtType.Text = String.Empty;
            this.txtText.Text = String.Empty;
            this.txtDetails.Text = String.Empty;

            if (this.exception == null) { return; }

            TreeNode root = this.CreateNode("$exception", this.exception);

            root.Nodes.AddRange(this.GetChildren(this.exception));

            root.Expand();

            this.trvDetails.Nodes.Add(root);

            this.trvDetails.BeforeExpand += this.OnTreeViewDetailsBeforeExpand;
            this.trvDetails.AfterSelect += this.OnTreeViewDetailsAfterSelect;

            this.trvDetails.SelectedNode = root;
        }

        private void OnTreeViewDetailsBeforeExpand(Object sender, TreeViewCancelEventArgs args)
        {
            if (this.IsFakeNode(args.Node) && args.Node.Tag is NodeData data)
            {
                this.AddChildren(args.Node, data);
            }
        }

        private void OnTreeViewDetailsAfterSelect(Object sender, TreeViewEventArgs args)
        {
            this.ApplyOverallInformation(args.Node);

            this.txtDetails.Text = args.Node.Tag?.ToString() ?? String.Empty;
        }

        private void OnMenuCopyClick(Object sender, EventArgs args)
        {
            if (sender is ToolStripMenuItem source)
            {
                if (source.Owner is ContextMenuStrip owner)
                {
                    if (owner.SourceControl is TextBox control)
                    {
                        try { Clipboard.SetText(control.Text); } catch { }
                    }
                }
            }
        }

        private Boolean IsFakeNode(TreeNode node)
        {
            return node != null && node.Nodes.Count > 0 && String.Equals(node.Nodes[0].Text, ErrorDetailsDialog.fakeNode);
        }

        private void AddChildren(TreeNode parent, NodeData source)
        {
            if (parent == null || source == null)
            {
                return;
            }

            parent.Nodes.Clear();

            if (source.Value is Exception exception)
            {
                parent.Nodes.AddRange(this.GetChildren(exception));
            }
            else if (source.Value is IDictionary dictionary)
            {
                parent.Nodes.AddRange(this.GetChildren(dictionary));
            }
        }

        private TreeNode CreateNode(String text, Object value)
        {
            return new TreeNode(text) { Tag = new NodeData(value) };
        }

        private TreeNode CreateNode(String text, Object value, Type type)
        {
            return new TreeNode(text) { Tag = new NodeData(value, type) };
        }

        private TreeNode[] GetChildren(Exception source)
        {
            List<TreeNode> result = new List<TreeNode>();

            if (source != null)
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty;

                foreach (PropertyInfo property in source.GetType().GetProperties(flags))
                {
                    String text = property.Name;
                    Object value = this.GetValue(property, source);
                    Type type = property.PropertyType;

                    TreeNode node = this.CreateNode(text, value, type);

                    if (type == typeof(Exception) && value != null)
                    {
                        node.Nodes.Add(new TreeNode(ErrorDetailsDialog.fakeNode));
                    }
                    else if (type == typeof(IDictionary) && (value as IDictionary).Count > 0)
                    {
                        node.Nodes.Add(new TreeNode(ErrorDetailsDialog.fakeNode));
                    }

                    result.Add(node);
                }
            }

            result.Sort(new TreeNodeComparer());

            return result.ToArray();
        }

        private TreeNode[] GetChildren(IDictionary source)
        {
            List<TreeNode> result = new List<TreeNode>();

            if (source != null)
            {
                foreach (Object key in source.Keys)
                {
                    result.Add(this.CreateNode(key.ToString(), source[key]));
                }
            }

            result.Sort(new TreeNodeComparer());

            return result.ToArray();
        }

        private Object GetValue(PropertyInfo property, Object source)
        {
            try
            {
                return property.GetValue(source);
            }
            catch
            {
                return "Unable to determine property value.";
            }
        }

        private void ApplyOverallInformation(TreeNode node)
        {
            if (node?.Tag is NodeData data)
            {
                if (data.Value is Exception exception)
                {
                    this.txtType.Text = exception.GetType().Name;
                    this.txtText.Text = exception.Message;
                }
                else
                {
                    this.ApplyOverallInformation(node.Parent);
                }
            }
        }

        private class TreeNodeComparer : IComparer<TreeNode>
        {
            public Int32 Compare([AllowNull] TreeNode x, [AllowNull] TreeNode y)
            {
                if (x == null && y == null) { return 0; }

                if (x == null && y != null) { return -1; }

                if (x != null && y == null) { return 1; }

                return String.Compare(x.Text, y.Text, StringComparison.InvariantCulture);
            }
        }

        private class NodeData
        {
            public NodeData(Object value)
                : this(value, null)
            {
            }

            public NodeData(Object value, Type type)
                : base()
            {
                this.Value = value;
                this.Type = type ?? value?.GetType() ?? typeof(Object);
            }

            public Object Value { get; }

            public Type Type { get; }

            public override String ToString()
            {
                return this.Value?.ToString() ?? String.Empty;
            }
        }
    }
}
