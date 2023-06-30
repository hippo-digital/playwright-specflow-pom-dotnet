using FluentAssertions;
using Microsoft.Playwright;

namespace PlaywrightSpecFlowPOM.Pages;

public class SearchResultsPage
{
    private readonly IPage _user;

    public SearchResultsPage(Hooks.Hooks hooks)
    {
        _user = hooks.User;
    }
    
    private int _resultIndex; //-> this is being set in the action/assertions below
    private ILocator SearchInput => _user.Locator("input[id='search_form_input']");
    private ILocator SearchResults => _user.Locator("div[id='links']");
    //Notice how the selector below uses the 'SearchResults' locator instead of the IPage to locate the element
    //The 'nth' locator is used to select an element at a specific index when there are multiple elements found
    private ILocator ResultArticle => SearchResults.Locator("article").Nth(_resultIndex);
    //We're using the single search result that we've located as 'ResultArticle' to locate the next 2 selectors
    private ILocator ResultHeading => ResultArticle.Locator("h2");
    private ILocator ResultLink => ResultArticle.Locator("a[data-testid='result-title-a']");

    public async Task AssertPageContent(string searchTerm)
    {
        //Assert the page url
        await _user.WaitForURLAsync($"https://duckduckgo.com/?q={searchTerm}*");

        //Assert the search input has the search term
        var searchInputInnerText = await SearchInput.InputValueAsync();
        searchInputInnerText.Should().Be(searchTerm);
    }
    
    public async Task AssertSearchResultAtIndex(string searchTerm, int resultIndex, string expectedResultLink)
    {
        
        _resultIndex = resultIndex;
        
        //Assert the first result text
        var firstResultInnerText = await ResultHeading.InnerTextAsync();
        firstResultInnerText.Should().Contain(searchTerm);
        
        //Assert the first result link
        var firstResultLink = await ResultLink.GetAttributeAsync("href");
        firstResultLink.Should().Be(expectedResultLink);
    }
}