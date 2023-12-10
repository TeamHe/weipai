using ResModel;
using System;
using System.Windows.Forms;

namespace GridBackGround.Forms
{
    public class menu_item_control
    {
        public ToolStripMenuItem ParentMenu { get; set; }


        public MainForm Parent { get; set; }


        public IPowerPole pole { get; set; }

        public ToolStripMenuItem AddDropDownMenuItem(string text)
        {
            return AddDropDownMenuItem(this.ParentMenu, text);
        }

        public ToolStripMenuItem AddDropDownMenuItem(string text,
                                                     EventHandler handler)
        {
            ToolStripMenuItem item = AddDropDownMenuItem(text);
            item.Tag = handler;
            item.Click += menu_click;
            return item;
        }

        public ToolStripMenuItem AddDropDownMenuItem(ToolStripMenuItem parent,
                                             string text)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(text);
            parent.DropDownItems.Add(item);

            return item;
        }

        public ToolStripMenuItem AddDropDownMenuItem(ToolStripMenuItem parent,
                                                     string text,
                                                     EventHandler handler)
        {
            ToolStripMenuItem item = AddDropDownMenuItem(parent, text);
            item.Tag = handler;
            item.Click += menu_click;
            return item;
        }

        private void menu_click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                EventHandler handler = item.Tag as EventHandler;
                if (handler != null)
                {
                    if ((this.pole = this.GetPowerPole()) == null)
                        return;
                    handler(sender, e);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Button Click event handle error:" + ex.Message);
            }
        }


        protected IPowerPole GetPowerPole() 
        {
            if(this.Parent == null)
                return null;
            return Parent.GetSeletedPole();
        }

        public menu_item_control(MainForm p_form, ToolStripMenuItem p_menu)
        {
            ParentMenu = p_menu;
            Parent = p_form;
        }

        public menu_item_control() { }

        public menu_item_control(MainForm p_form)
        {
            Parent = p_form;
        }

    }
}
