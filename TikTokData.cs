using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


public class TikTokData {

    private string urlBase = "https://www.tiktok.com/";
    private string userName = "";
    private string pageSource;

    private TikTokData( string username, string pageContent ) {

        userName = username;
        pageSource = pageContent;

    }

    private async Task<string> GetPageContentAsync() {

        try {

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add( "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36" );
            return await client.GetStringAsync( urlBase + userName );

        } catch (HttpRequestException httpEx) {

            Console.WriteLine( $"HTTP Request Error: {httpEx.Message}" );
            //return "HTTP Request Error: " + httpEx.Message;

        } catch (Exception ex) {

            Console.WriteLine( $"An unexpected error occurred: {ex.Message}" );
            //return "An unexpected error occurred: " + ex.Message;

        }

        return string.Empty;

    }

    public static async Task<TikTokData> CreateAsync( string username = "" ) {

        if ( !string.IsNullOrEmpty( username ) ) {

            TikTokData tikTokData = new TikTokData( username, await new TikTokData( username, "" ).GetPageContentAsync() );
            return tikTokData;

        }

        return null;

    }

    public int GetData( string type ) {

        if ( !string.IsNullOrEmpty( type ) ) {

            string strPattern;

            if ( type == "followers" ) { strPattern = "followerCount"; }
            else if ( type == "following" ) {  strPattern = "followingCount"; }
            else if ( type == "friends" ) {  strPattern = "friendCount"; }
            else if ( type == "likes" ) {  strPattern = "heartCount"; }
            else { return 0; }

            try {

                string pattern = $"\"{strPattern}\":(\\d+)([}},])";
                Match match = Regex.Match( pageSource, pattern );

                if ( match.Success ) {

                    return int.Parse(match.Groups[1].Value);

                }
            } catch ( Exception ex ) {

                Console.WriteLine( $"Error extracting follower count: {ex.Message}" );
                //return "Error extracting follower count: " + ex.Message;

            }

        }

        return 0;

    }

}
