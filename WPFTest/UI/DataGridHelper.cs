using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTest.UI
{
    public static class DataGridHelper
    {
       
            public static void SetRealTimeCommit(DataGrid dataGrid, bool isRealTime)
            {
                dataGrid.SetValue(RealTimeCommitProperty, isRealTime);
            }

            public static bool GetRealTimeCommit(DataGrid dataGrid)
            {
                return (bool)dataGrid.GetValue(RealTimeCommitProperty);
            }

            public static readonly DependencyProperty RealTimeCommitProperty =
            DependencyProperty.RegisterAttached("RealTimeCommit", typeof(bool),
            typeof(DataGridHelper),
            new PropertyMetadata(false, RealTimeCommitCallBack));

            private static void RealTimeCommitCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                var dg = d as DataGrid;
                if (dg == null)
                    return;
                EventHandler<DataGridCellEditEndingEventArgs> ceHandler = delegate (object xx, DataGridCellEditEndingEventArgs yy)
                {
                    var flag = GetRealTimeCommit(dg);
                    if (!flag)
                        return;
                    var cellContent = yy.Column.GetCellContent(yy.Row);
                    if (cellContent != null && cellContent.BindingGroup != null)
                        cellContent.BindingGroup.CommitEdit();
                };
                dg.CellEditEnding += ceHandler;
                RoutedEventHandler eh = null;
                eh = (xx, yy) =>
                {
                    dg.Unloaded -= eh;
                    dg.CellEditEnding -= ceHandler;
                };
                dg.Unloaded += eh;
            }
        }
}
