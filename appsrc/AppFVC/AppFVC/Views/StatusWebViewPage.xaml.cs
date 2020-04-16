//
//
// Author:
//      Alessandro de Oliveira Binhara (binhara@azuris.com.br)
//
// Dual licensed under the terms of the MIT or GNU GPL
//
// Copyright 2019-2020 Azuris Mobile & Cloud System
//
using System;
using Xamarin.Forms;

namespace AppFVC.Views
{
    public partial class StatusWebViewPage : ContentPage
    {
        public StatusWebViewPage()
        {
            InitializeComponent();
        }

        private void PagOnNavigating(object sender, WebNavigatingEventArgs e)
        {
            lblStatus.Text = "Carregando página...";
            lblStatus.IsVisible = true;
            btnRefresh.IsVisible = false;
        }
        private void PagOnNavigated(object sender, WebNavigatedEventArgs e)
        {
            lblStatus.IsVisible = false;
            btnRefresh.IsVisible = true;
        }
        private void btnBackClicked(object sender, EventArgs e)
        {
            // Verifica se exite uma página para retornar
            //if (browser.CanGoBack)
            //{
                browser.GoBack();
            //}
        }
        private void btnForwardClicked(object sender, EventArgs e)
        {
            //navega para frente
            if (browser.CanGoForward)
            {
                browser.GoForward();
            }
        }

        private void btnRefreshClicked(object sender, EventArgs e)
        {
            lblStatus.Text = "Carregando página...";
            lblStatus.IsVisible = true;
            btnRefresh.IsVisible = false;
            browser.Reload();
            lblStatus.IsVisible = false;
            btnRefresh.IsVisible = true;
        }
    }
}
