﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    public partial class OrderHistoryPage : ContentPage
    {
        private string Selected_orderID;
        public OrderHistoryPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            string ord = "1";
            var getorders = await MobileService.GetTable<TBL_Orders>().Where(orders => orders.stat.Contains(ord) && orders.users_id.ToLower().Contains(user_id)).ToListAsync();
            OrdersList.ItemsSource = getorders;
            OrderDetailsList.ItemsSource = null;
            lblsum.Text = "Php. 0";
        }

        private async void OrdersList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Selected_orderID = (e.CurrentSelection.FirstOrDefault() as TBL_Orders)?.id;
                await XGetOrders();
            }
            catch
            {
                //ignored
            }
        }

        private async Task XGetOrders()
        {
            var getorderdetails = (await MobileService.GetTable<V_Orders>().Where(orders => orders.order_id == Selected_orderID).ToListAsync());
            OrderDetailsList.ItemsSource = getorderdetails;
            var amount = getorderdetails.Sum(p => p.sub_total).ToString("N");
            //AnimateText();
            lblsum.Text = "Php. " + amount;
        }

        private void AnimateAmount()
        {
            var stw = new Stopwatch();
            stw.Start();
            Device.StartTimer(TimeSpan.FromSeconds(1 / 100f), () =>
            {
                double t = stw.Elapsed.TotalMilliseconds % 500 / 500;

                return true;
            });
        }

        private void Reload_OnClicked(object sender, EventArgs e)
        {
            OnAppearing();
           
        }
    }
}