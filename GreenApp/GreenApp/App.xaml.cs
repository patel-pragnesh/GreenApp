﻿using System;
using GreenApp.Activity;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenApp
{
    public partial class App
    {
        public static readonly MobileServiceClient MobileService = new MobileServiceClient("https://greenmarketwebapp.azurewebsites.net");
        //Transaction info
        public static string Selected_ProdId;
        public static string Selected_CatID, cat_desc;
        public static string user_id;
        public static string CurrentOrderId;
        public static bool refresh;
        public static string MarketStatus;
        public static bool checkout;
        public static bool SignedIn;
        public static string Selected_orderID;
        //Customer info
        public static string fullname, mobilenum, emailadd, password, propic,picstr;
        public static DateTime datereg;
        public static bool historyloaded = false;
        //Order delivery info
        public static double order_lat;
        public static double order_long;
        public static string order_rcvr_add;
        public static string order_notes;
        //public static string order_
        //network
        public static bool hasnetwork;

        public App()
        {
            InitializeComponent();
            Device.SetFlags(new []{"Brush_Experimental"});
            Device.SetFlags(new[] { "SwipeView_Experimental" }); // Add here
            //Device.BeginInvokeOnMainThread(() => { Xamarin.Forms.Application.Current.MainPage = new AppShell(); });
            MainPage = new NavigationPage(new MenuPage());
        }
        
        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
