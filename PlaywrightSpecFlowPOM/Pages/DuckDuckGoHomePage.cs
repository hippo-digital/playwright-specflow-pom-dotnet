using FluentAssertions;
using Microsoft.Playwright;

namespace PlaywrightSpecFlowPOM.Pages;

public class DuckDuckGoHomePage
{
    private readonly IPage _user;

    public DuckDuckGoHomePage(Hooks.Hooks hooks)
    {
        _user = hooks.User;
    }

    private ILocator SearchInput => _user.Locator("input[id='search_form_input_homepage']");
    private ILocator SearchButton => _user.Locator("input[id='search_button_homepage']");
    
    public async Task AssertPageContent()
    {
        //Assert that the correct URL has been reached
        _user.Url.Should().Be("https://duckduckgo.com/?");
        
        //Assert that the search input is visible
        var searchInputVisibility = await SearchInput.IsVisibleAsync();
        searchInputVisibility.Should().BeTrue();
        
        // //Assert that the search button is visible
        var searchBtnVisibility = await SearchButton.IsVisibleAsync();
        searchBtnVisibility.Should().BeTrue();
    }

    public async Task SearchAndEnter(string searchTerm)
    {
        //Type the search term into the search input
        await SearchInput.TypeAsync(searchTerm);
        
        //Assert that the search input has the text entered
        var searchInputInnerText = await SearchInput.InputValueAsync();
        searchInputInnerText.Should().Be(searchTerm);
        
        //Click the search button to submit the search
        await SearchButton.ClickAsync();
    }
}