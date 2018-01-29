﻿using Eto;
using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juniansoft.Samariterm.EtoForms.Resources;

namespace Juniansoft.Samariterm.EtoForms.MenuCommands
{
    public class NewCommand: Command
    {
        public NewCommand()
        {
            this.MenuText = "&New";
            if (!Platform.Instance.IsWinForms)
                this.ToolBarText = "New";
            this.ToolTip = "New";
            this.Image = AppResources.DocumentNew;
            this.Shortcut = Application.Instance.CommonModifier | Keys.N;
        }
    }
}
