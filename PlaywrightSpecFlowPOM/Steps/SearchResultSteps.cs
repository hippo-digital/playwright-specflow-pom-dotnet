using PlaywrightSpecFlowPOM.Pages;

namespace PlaywrightSpecFlowPOM.Steps;

[Binding]
public class SearchResultSteps
{
    private readonly SearchResultsPage _searchResultsPage;

    public SearchResultSteps(SearchResultsPage searchResultsPage)
    {
        _searchResultsPage = searchResultsPage;
    }

    [Then(@"the search results show '(.*)' as the first result with link '(.*)'")]
    public async Task ThenTheSearchResultsShowAsTheFirstResult(string searchTerm, string expectedLink)
    {
        //Assert the page content
        await _searchResultsPage.AssertPageContent(searchTerm);
        
        //Assert the first search result (hence the index of 0)
        await _searchResultsPage.AssertSearchResultAtIndex(searchTerm, 0, expectedLink);
    }
}