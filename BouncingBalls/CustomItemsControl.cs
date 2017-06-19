using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BouncingBalls
{
    class CustomItemsControl : ItemsControl
    {
        // http://drwpf.com/blog/category/item-containers/
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is ContentPresenter);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ContentPresenter();
        }
    }
}
