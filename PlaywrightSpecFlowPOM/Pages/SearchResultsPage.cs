using Microsoft.Playwright;

namespace PlaywrightSpecFlowPOM.Pages;

public class SearchResultsPage
{
    private readonly IPage _user;

    public SearchResultsPage(Hooks.Hooks hooks)
    {
        _user = hooks.User;
    }
    private ILocator SearchInput => _user.Locator("input[id='search_form_input']");
    private ILocator ResultArticle(int nth) => _user.GetByTestId("result").Nth(nth);
    //We're using the single search result that we've located as 'ResultArticle' to locate the next 2 selectors
    private ILocator ResultHeading(int nth) => ResultArticle(nth).Locator("h2");
    private ILocator ResultLink(int nth) => ResultArticle(nth).Locator("a[data-testid='result-title-a']");

    public async Task AssertPageContent(string searchTerm)
    {
        await _user.WaitForURLAsync($"https://duckduckgo.com/?va=v&t=ha&q={searchTerm}&ia=web");
        await Assertions.Expect(SearchInput).ToHaveValueAsync(searchTerm);
    }
    
    public async Task AssertSearchResultAtIndex(string searchTerm, int resultIndex, string expectedResultLink)
    {
        await Assertions.Expect(ResultHeading(resultIndex)).ToContainTextAsync(searchTerm);
        await Assertions.Expect(ResultLink(resultIndex)).ToHaveAttributeAsync("href", expectedResultLink);
    }
}