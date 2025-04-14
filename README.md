# Unity: Retrieve Data From TikTok

From Unity retrieve TikTok data such as the number of followers, following, and friends.

### Example calling the function when the script starts (the function must be async)

```HTML
private async void Start() {

   string username = "@username";

   TikTokData tiktok = await TikTokData.CreateAsync( username );

   int followers = tiktok.GetData( "followers" );
   int following = tiktok.GetData( "following" );
   int friends = tiktok.GetData( "friends" );

   Debug.Log( $"Followers: {followers}" );
   Debug.Log( $"Following: {following}" );
   Debug.Log( $"Friends: {friends}" );

}
```
