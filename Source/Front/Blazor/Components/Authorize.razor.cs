using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.ApiResources;
using Blazor.Handlers;
using Blazor.Helpers;
using Blazor.Models;
using Blazor.States.ApiToken;
using Blazored.LocalStorage;
using BlazorState;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace Blazor.Components
{
    public partial class Authorize : BlazorStateComponent
    {
        [Parameter] public  RenderFragment           ChildContent      { get; set; }
        [Inject]    private NavigationManager        NavigationManager { get; set; }
        [Inject]    private Endpoints                Endpoints         { get; set; }
        [Inject]    private ISyncLocalStorageService LocalStorage      { get; set; }
        [Inject]    private HttpClient               HttpClient        { get; set; }
        [Inject]    private ISender                  Sender            { get; set; }

        private RenderFragment? _authorizedComponent = null;

        protected override async Task OnInitializedAsync()
        {
            var authState = GetState<AuthenticationState>();

            if (authState.Authenticated is true)
                AuthorizeView();
            else if (authState.Expired is true)
                await SilentLogin();
            else
                NavigateToLoginPage();
        }

        private async Task SilentLogin()
        {
            var refreshToken         = LocalStorage.GetItem<string>(LocalStorageKeys.RefreshToken);
            var userId               = LocalStorage.GetItem<long>(LocalStorageKeys.UserId);
            var refreshTokenResource = new RefreshTokenResource(userId, refreshToken);
            var response =
                await HttpClient.PostAsync<JwtTokenResource>(Endpoints.Server.RefreshToken, refreshTokenResource);
            if (response.Success is false)
            {
                NavigateToLoginPage();
                return;
            }

            await Sender.Send(new StoreJwtTokenRequest(response.Data!.Token));
            AuthorizeView();
        }

        private void AuthorizeView()
        {
            _authorizedComponent = ChildContent;
        }

        private void NavigateToLoginPage()
        {
            NavigationManager.NavigateTo(Endpoints.Client.Login);
        }
    }
}