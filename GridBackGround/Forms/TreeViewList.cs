using System.Collections.Generic;
using System.Windows.Forms;
using ResModel.EQU;

namespace GridBackGround.Forms
{
    public class TreeViewList
    {
        public TreeNodeCollection ParentNodes { get; set; }

        public List<Line> Lines { get; set; }

        public TreeNode SelectedTreeNode { get; set; }

        public int SelectedEquID {  get; set; }

        public int SelectedTowerID { get; set; }

        public int SelectedLineID { get; set; }

        public bool HasTypeNode { get; set; }

        private TreeNode _line_get_parent(DevFlag flag)
        {
            if(this.ParentNodes == null)
                return null;

            foreach(TreeNode node in  this.ParentNodes)
            {
                if(node.Tag == null) continue;
                if (flag == (DevFlag)node.Tag)
                    return node;
            }
            return null;
        }

        public bool Add_equ(TreeNode parent, Equ equ)
        {
            TreeNode node = new TreeNode();
            node.Text = equ.Name;
            node.Tag = equ;
            node.ToolTipText = equ.ToString();
            parent.Nodes.Add(node);

            if (equ.ID != SelectedEquID && equ.ID > 0)
                return false;
            this.SelectedTreeNode = node;
            return true;
        }

        public bool Add_equs(TreeNode parent, List<Equ> equs)
        {
            bool selected = false;
            if (parent==null || equs == null)
                return false;
            foreach(Equ equ in equs)
            {
                if(Add_equ(parent,equ))
                    selected = true;
            }
            if(selected)
                parent.Expand();
            return selected;
        }



        public bool Add_Tower(TreeNode parent, Tower tower)
        {
            bool selected = false;
            TreeNode node = new TreeNode();
            node.Text = tower.TowerName;
            node.Tag = tower;
            node.ToolTipText = tower.ToString();
            if(SelectedTowerID >0 && SelectedTowerID == tower.TowerNO)
            {
                this.SelectedTreeNode = node;
                selected = true;
            }
            parent.Nodes.Add(node);
            bool sel_equ = Add_equs(node, tower.EquList);
            return sel_equ || selected;
        }

        public bool Add_towers(TreeNode parent, List<Tower> towers)
        {
            bool selected = false;
            if (parent == null || towers == null)
                return false;
            foreach (Tower tower in towers)
            {
                if (Add_Tower(parent, tower))
                    selected = true;
            }
            if(selected)
                parent.Expand();
            return selected;
        }


        public bool Add_line(TreeNodeCollection parent, Line line)
        {
            bool selected  = false;
            TreeNode node = new TreeNode();
            parent.Add(node);
            node.Name = line.NO.ToString();
            node.Text = line.Name;
            node.ToolTipText = line.ToString();
            node.Tag = line;
            if(this.SelectedLineID >0 && this.SelectedLineID == line.NO)
            {
                this.SelectedTreeNode = node;
                selected = true;
            }
            bool sel_tower = Add_towers(node, line.TowerList);
            return selected || sel_tower;
        }

        public bool Add_lines(TreeNodeCollection nodes, List<Line> lines)
        {
            TreeNodeCollection parent = nodes;
            TreeNode pNode = null;
            bool selected = false;
            if(nodes == null || lines == null) return false;
            foreach (Line line in lines)
            {
                if (this.HasTypeNode)
                {
                    if ((pNode = this._line_get_parent(line.Flag)) == null)
                        continue;
                     parent = pNode.Nodes;
                }
                if (Add_line(parent, line))
                {
                    selected = true;
                    if (parent != null)
                        pNode.Expand();
                }
            }
            return selected;
        }
        public bool Add_lines()
        {
            return this.Add_lines(ParentNodes, Lines);
        }


        public static bool LineList(TreeNodeCollection nodes, List<Line> linelist, 
            out TreeNode selectedNode, 
            int SelectedEquID = 0, 
            int SelectedTowerID = 0, 
            int SelectedLineID = 0)
        {
            TreeViewList tree = new TreeViewList()
            {
                ParentNodes = nodes,
                Lines = linelist,
                SelectedEquID = SelectedEquID,
                SelectedTowerID = SelectedTowerID,
                SelectedLineID = SelectedLineID,
            };
            bool ret =tree.Add_lines();
            selectedNode = tree.SelectedTreeNode;
            return ret;
        }
    }
}
