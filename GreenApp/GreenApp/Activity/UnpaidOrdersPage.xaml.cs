﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static GreenApp.App;

namespace GreenApp.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnpaidOrdersPage : ContentPage
    {
        List<SwipeView> swipeViews { set; get; }
        List<SwipeView> swipViews { set; get; }
        private SwipeView XSwipeViews;
        public UnpaidOrdersPage()
        {
            InitializeComponent();
            //swipeViews = new List<SwipeView>();
        }

        protected override async void OnAppearing()
        {
            await getHistoryOrders();
        }

        private async Task getHistoryOrders()
        {
            try
            {
                xRefreshView.IsRefreshing = true;
                var getorders = await MobileService.GetTable<TBL_Orders>().Where(orders => orders.users_id.ToLower().Contains(user_id) && !orders.stat.ToLower().Contains("2")).ToListAsync();
                OrdersList.ItemsSource = getorders;
                Selected_orderID = null;
                OrdersList.SelectedItem = null;
                xRefreshView.IsRefreshing = false;
            }
            catch
            {
                await Navigation.PushAsync(new NoInternetPage(), true);
            }
        }
        private void OrdersList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersList.SelectedItem == null) return;
            Selected_orderID = (e.CurrentSelection.FirstOrDefault() as TBL_Orders)?.id;
            //await XGetOrders();
        }

        private async void RefreshView_OnRefreshing(object sender, EventArgs e)
        {
            await getHistoryOrders();
        }
        //private void XSwipeViews_OnSwipeStarted(object sender, SwipeStartedEventArgs e)
        //{
        //    if (swipeViews.Count == 1)
        //    {
        //        swipeViews[0].Close();
        //        swipeViews.Remove(swipeViews[0]);
        //    }
        //}

        //private void XSwipeViews_OnSwipeEnded(object sender, SwipeEndedEventArgs e)
        //{
        //    if (swipeViews.Count == 1)
        //    {
        //        swipeViews.Add(swipeViews[0]);
        //    }
            
        //}
    }
}