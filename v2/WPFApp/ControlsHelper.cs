﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WPFApp
{
    public static class ControlsHelper
    {

        public static Grid CreateGrid(int numRows, int numCols)
        {
            // grid definition
            var grid = new Grid();

            for (int i = 0; i < numRows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50, GridUnitType.Pixel) });
            }
            for (int j = 0; j < numCols; j++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Pixel) });
            }

            // column headers
            for (int j = 0; j < numCols - 1; j++)
            {
                TextBlock colHeader = new TextBlock();
                colHeader.Text = (j + 1).ToString(); // Column numbering starts at 1
                colHeader.TextAlignment = TextAlignment.Center;
                colHeader.FontWeight = FontWeights.Bold;
                colHeader.Margin = new Thickness(0, 0, 0, 10);
                Grid.SetRow(colHeader, 0);
                Grid.SetColumn(colHeader, j + 1);
                grid.Children.Add(colHeader);
            }

            // row headers
            for (int i = 0; i < numRows - 1; i++)
            {
                TextBlock rowHeader = new TextBlock();
                rowHeader.Text = ((char)('A' + i)).ToString(); // Row lettering starts at A
                rowHeader.TextAlignment = TextAlignment.Center;
                rowHeader.FontWeight = FontWeights.Bold;
                rowHeader.Margin = new Thickness(0, 0, 10, 0);
                rowHeader.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(rowHeader, i + 1);
                Grid.SetColumn(rowHeader, 0);
                grid.Children.Add(rowHeader);
            }

            // buttons
            for (int i = 1; i < numRows; i++)
            {
                for (int j = 1; j < numCols; j++)
                {
                    Button button = new Button();
                    button.Content = "";
                    button.Background = new SolidColorBrush(Colors.LightBlue);
                    button.Click += GridButton_Click;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    grid.Children.Add(button);
                }
            }
            grid.Name = "grid";
            return grid;
        }

        private static void GridButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            Grid parentGrid = FindParentGrid(clickedButton);
            int row = Grid.GetRow(clickedButton);
            int col = Grid.GetColumn(clickedButton);
            MessageBox.Show("The clicked button is in row " + row + " and column " + col + ".");
        }

        private static Grid FindParentGrid(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !(parent is Grid && ((Grid)parent).Name == "grid"))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return (Grid)parent;
        }
    }
}