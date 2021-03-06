﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenApp.Animation;
using GreenApp.Models;
using GreenApp.Utils;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static GreenApp.App;

namespace GreenApp.Activity
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
            //BindingContext = new CheckInternetModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            emailentry.Text = Settings.LastUsedEmail;
        }
        
        private async void Btnlogin_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (indicatorloader.IsVisible) return;
                indicatorloader.IsVisible = true;
                //await Navigation.PushModalAsync(new MenuPage(),true);
                bool isemailempty = string.IsNullOrEmpty(emailentry.Text);
                bool ispasswordempty = string.IsNullOrEmpty(passentry.Text);
                if (isemailempty || ispasswordempty)
                {
                    indicatorloader.IsVisible = false;
                    await DisplayAlert("Error", "Please enter your email and password!", "OK");
                }
                else
                {
                    indicatorloader.IsVisible = true;
                    var stat = (await MobileService.GetTable<TBL_MarketStatus>().ToListAsync()).FirstOrDefault();
                    if (stat != null) MarketStatus = stat.status;
                    var users = (await MobileService.GetTable<TBL_Users>().Where(mail => mail.emailadd == emailentry.Text).ToListAsync()).FirstOrDefault();
                    if (users != null)
                    {
                        if (users.password == passentry.Text)
                        {
                            if (MarketStatus == "1")
                            {
                                var getAddresses = (await MobileService.GetTable<TBL_Addresses>().Where(p => p.user_id == users.Id).ToListAsync()).FirstOrDefault();
                                if (getAddresses != null) AddressesPage._selectedAddressId = getAddresses.id;
                                user_id = users.Id;
                                fullname = users.full_name;
                                mobilenum = users.mobile_num;
                                emailadd = users.emailadd;
                                password = users.password;
                                datereg = users.datereg;
                                propic = users.propic;
                                picstr = users.picstr;
                                //user_id = null;
                                CurrentOrderId = null;
                                refresh = false;
                                indicatorloader.IsVisible = false;
                                Settings.LastUsedEmail = emailentry.Text;
                                SignedIn = true;
                                await Navigation.PopToRootAsync(true);
                                //Device.BeginInvokeOnMainThread(() => { Application.Current.MainPage = new MenuPage(); });
                                ////PROBLEM HERE LOGING OUT AND NOT REMEMBERING EMAIL
                                //await Navigation.PushAsync(new MenuPage(), true);
                            }
                            else
                            {
                                indicatorloader.IsVisible = false;
                                await Navigation.PushAsync(new MarketClosePage());
                            }
                        }
                        else
                        {
                            indicatorloader.IsVisible = false;
                            await DisplayAlert("Error", "Email or password is incorrect!", "OK");
                        }
                    }
                    else
                    {
                        indicatorloader.IsVisible = false;
                        await DisplayAlert("Error", "There was an error logging you in! Please check the information you're entering.", "OK");
                    }
                }
            }
            catch
            {
                indicatorloader.IsVisible = false;
                await Navigation.PushAsync(new NoInternetPage());
            }
        }

        private async void Btnsignup_OnClicked(object sender, EventArgs e)
        {
            //Settings.LastUsedEmail = emailentry.Text;
            await Navigation.PushAsync(new SignupPage(),true);
        }
    }
}