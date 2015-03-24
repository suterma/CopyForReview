﻿// 
//     Copy for review, code sharing made simple.
//     Copyright (C) 2015 by marcel suter, marcel@codeministry.ch
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Linq;
using System.Windows;
using Codeministry.CopyForReview.Formatters;

namespace Codeministry.CopyForReview.Controls {
    /// <summary>
    ///     A selector for a formatter
    /// </summary>
    public partial class FormatSelector {
        /// <summary>
        ///     Gets the selected formatter.
        /// </summary>
        /// <value>
        ///     The selected formatter.
        /// </value>
        public IFormatter SelectedFormatter { get; private set; }

        /// <summary>
        ///     Initializes the formatter buttons.
        /// </summary>
        /// <param name="selectedFormatterName">Name of the selected formatter.</param>
        private void InitializeFormatterButtons(String selectedFormatterName) {
            //Initialize Formatter Buttons
            var formatters = Factory.GetFormatters();
            foreach (var formatter in formatters) {
                var button = new FormatterButton(formatter);
                button.Click += ButtonFormatter_Click;
                StackPanelFormatters.Children.Add(button);
                if (formatter.Name == selectedFormatterName) {
                    button.SetDefault();
                    SelectedFormatter = formatter;
                }
            }

            if ((SelectedFormatter == null) && formatters.Any()) {
                //just select the first one
                var firstButton = StackPanelFormatters.Children.Cast<FormatterButton>().First<FormatterButton>();
                firstButton.SetDefault();
                SelectedFormatter = firstButton.Formatter;
            }
        }

        


        /// <summary>
        ///     Initializes a new instance of the <see cref="FormatSelector" /> class.
        /// </summary>
        /// <param name="selectedFormatterName">Name of the selected formatter.</param>
        /// <param name="isSelectFullLines">if set to <c>true</c> [is select full lines].</param>
        /// <param name="isDeindent">if set to <c>true</c>, the code is deindented.</param>
        public FormatSelector(string selectedFormatterName, bool isSelectFullLines, bool isDeindent) {
            InitializeComponent();
            InitializeFormatterButtons(selectedFormatterName);

            CheckBoxSelectionFullLines.IsChecked = isSelectFullLines;
            CheckBoxDeindent.IsChecked = isDeindent;
        }

        /// <summary>
        /// Handles the Click event of the ButtonFormatted control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ButtonFormatter_Click(object sender, RoutedEventArgs e) {
            //find the formatter in question and invoke it
            var button = sender as FormatterButton;
            SelectedFormatter = Factory.GetFormatters().Single(item => item.Name == button.Formatter.Name); //TODO later use better matching, possibly using a GUID
            DialogResult = true;
            Close();
        }

        /// <summary>
        ///     Gets a value indicating whether this full lines should be selected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if full lines should be selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelectionFullLines {
            get { return (CheckBoxSelectionFullLines.IsChecked == true); }
        }

        /// <summary>
        ///     Gets a value indicating whether deindent is selected.
        /// </summary>
        /// <value>
        ///     <c>true</c> if deindent is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeindent {
            get { return (CheckBoxDeindent.IsChecked == true); }
        }
    }
}