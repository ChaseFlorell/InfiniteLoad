using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace InfiniteLoad
{
    public class InfiniteLoadBehavior : Behavior<ListView>
    {
        /// <summary>
        /// number of items remaining in the visible list before invoking <see cref="m:IPageable.LoadNextPage"/>.
        /// </summary>
        /// <remarks>defaults to zero, and new items will load when the user gets to the bottom of the list.</remarks>
        public int ItemsRemainingBuffer { private get; set; }
        
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ItemAppearing += OnItemAppearing;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemAppearing -= OnItemAppearing;
        }

        private async void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var listView = (ListView) sender;
            if(!(listView.BindingContext is IPageable pageable))
                throw new InvalidOperationException($"{nameof(BindingContext)} must be of type {nameof(IPageable)}");

            if(!(listView.ItemsSource is IList itemsSource))
            {
                // performance hit, just make the thing a list to begin with
                itemsSource = listView.ItemsSource?.Cast<object>().ToList() ?? 
                    throw new InvalidOperationException($"{nameof(ListView.ItemsSource)} cannot be converted to {nameof(IList<object>)}");
            }
            
            if(pageable.IsLoadingPage || itemsSource.Count == 0)
                return;

            //hit bottom!
            pageable.IsLoadingPage = true;
            var itemIndex = itemsSource.IndexOf(e.Item);
            if(itemIndex >= itemsSource.Count - ItemsRemainingBuffer)
                await pageable.LoadNextPage().ConfigureAwait(true);
            pageable.IsLoadingPage = false;
        }
    }
}