using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResModel.EQU;

namespace GridBackGround.Forms
{
    public class TreeViewList
    {
        public static bool LineList(TreeNodeCollection nodes, List<Line> linelist, out TreeNode selectedNode, int SelectedEquID = 0, int SelectedTowerID = 0, int SelectedID = 0)
        {
            bool state = false;
            selectedNode = null;
            nodes.Clear();
            if(linelist == null) return state;

            linelist.Sort((x,y)=>x.Name.CompareTo(y.Name));
            TreeNode selNode;
            foreach (Line line in linelist)
            {
                TreeNode node = new TreeNode();
                node.Name = line.NO.ToString();
                node.Text = line.Name;
                node.ToolTipText = line.ToString() ;
                node.Tag = line;
                nodes.Add(node);
                if (SelectedID == line.NO)
                    selectedNode = node;
                if (TowerList(node.Nodes, line.TowerList, out selNode,SelectedEquID))
                {
                    selectedNode = selNode;
                    node.Expand();
                    state = true;
                }
            }
            return state;
        }

        public static bool TowerList(TreeNodeCollection nodes, List<Tower> towerlist, out TreeNode selectedNode, int SelectedEquID = 0, int SelectedID = 0)
        {
            bool state = false;
            selectedNode = null;
            if (towerlist == null)
                return false;
            TreeNode selNode;
            nodes.Clear();
            towerlist.Sort((x, y) => x.TowerName.CompareTo(y.TowerName));
            foreach (Tower tower in towerlist)
            {
                TreeNode node = new TreeNode();
                node.Text = tower.TowerName;
                node.Tag = tower;
                node.ToolTipText = tower.ToString();
                nodes.Add(node);
                if (SelectedID == tower.TowerNO)
                {
                    state = true;
                    node.Expand();
                    selectedNode = node;
                }

                if (EquList(node.Nodes, tower.EquList,out selNode,SelectedEquID))
                {
                    selectedNode = selNode;
                    node.Expand();
                    state = true;
                }
               
            }
            return state;
        }

        public static bool EquList(TreeNodeCollection nodes, List<Equ> equList,out TreeNode selectedNode,int SelectedID = 0)
        {
            bool state = false;
            selectedNode = null;
            if (equList == null) return false;

            nodes.Clear();
            equList.Sort((x, y) => x.Name.CompareTo(y.Name));
            foreach (Equ equ in equList)
            {
                TreeNode node = new TreeNode();
                node.Text = equ.Name;
                node.Tag = equ;
                node.ToolTipText = equ.ToString();
                if (equ.ID == SelectedID)
                { 
                    state = true;
                    node.Expand();
                    selectedNode = node;
                }
                nodes.Add(node);
            }
            return state;
        }

    }
}
