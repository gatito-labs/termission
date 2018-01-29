﻿using Eto.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto;
using Juniansoft.Samariterm.EtoForms.Resources;

namespace Juniansoft.Samariterm.EtoForms.MenuCommands
{
    public class CompileCommand: Command
    {
        public CompileCommand()
        {
            this.MenuText = "Compile";
            if (!Platform.Instance.IsWinForms)
                this.ToolBarText = "Compile";
            this.ToolTip = "Compile";
            this.Image = AppResources.MediaStart;
            if (Platform.Instance.IsMac)
                this.Shortcut = Application.Instance.CommonModifier | Keys.B;
            else
                this.Shortcut = Keys.F5;
        }
    }
}
