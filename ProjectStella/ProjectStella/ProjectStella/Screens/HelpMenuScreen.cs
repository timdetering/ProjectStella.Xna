﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace ProjectStella
{
    class HelpMenuScreen : MenuScreen
    {
        #region Fields

        MenuEntry backMenuEntry;

        #endregion

        #region Initialize

        /// <summary>
        /// Constructor
        /// </summary>
        public HelpMenuScreen()
            : base("Help", "Normal")
        {
            backMenuEntry = new MenuEntry(string.Empty);

            SetMenuText();

            backMenuEntry.Selected += BackMenuEntrySelected;

            MenuEntries.Add(backMenuEntry);
        }

        /// <summary>
        /// Sets the menu entry text.
        /// </summary>
        void SetMenuText()
        {
            backMenuEntry.Text = Strings.Back;
        }

        #endregion

        #region Handle Input

        /// <summary>
        /// Event handler for when the Back menu entry is selected.
        /// </summary>
        void BackMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            BlankTransitionScreen.Load(ScreenManager, false, e.PlayerIndex, new BackgroundScreen(), new MainMenuScreen());
        }

        /// <summary>
        /// Overrides the OnCancel method to prevent the user from getting
        /// stuck in a blank screen.
        /// </summary>
        protected override void OnCancel(Microsoft.Xna.Framework.PlayerIndex playerIndex)
        {
            base.OnCancel(playerIndex);

            BlankTransitionScreen.Load(ScreenManager, false, playerIndex, new BackgroundScreen(), new MainMenuScreen());
        }

        #endregion
    }
}
