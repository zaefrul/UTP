& 'C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\15\CONFIG\POWERSHELL\Registration\\sharepoint.ps1'
#Get All Webs (sites)
$webs = Get-SPWebApplication "https://ushare.utp.edu.my" | Get-SPSite -Limit All | Get-SPWeb -Limit All
  $count=0
 #Iterate through webs
 foreach ($web in $webs)
 {
  #Get All Pages from site's Root into $AllPages Array
  $AllPages = @($web.Files | Where-Object {$_.Name -match "Home.aspx"})
  
  #Search All Folders for Pages
  foreach ($folder in $web.Folders)
      {
          #Add the pages to $AllPages Array
          $AllPages += @($folder.Files | Where-Object {$_.Name -match "Home.aspx"})
      }
    
   #Iterate through all pages
   foreach($Page in $AllPages)
     {
        $Page.CheckOut();
        #Web Part Manager to get all web parts from the page
        $webPartManager = $web.GetLimitedWebPartManager($Page.ServerRelativeUrl, [System.Web.UI.WebControls.WebParts.PersonalizationScope]::Shared)
        
       #Iterate through each web part
       foreach($webPart in $WebPartManager.WebParts)
          {
            $OldTitle=$webPart.title
            #Get the Content Editor web part with specific Title
            
            if($webPart.title -like "UShare - Announcement")
             {
                  #$count = $count + 1;
                  #Replace the Old Title
                  #$webPart.title = $webPart.title.Replace("Crescent Inc.", "Lunar Inc.")
                  $webPart.SiteCollection = $Page.Web.Url
                  $webPart.ListName = "Announcements"
                  #Same method goes to update any other custom properties.
                  #E.g. To update Page viewer web part's link property:
                  #$webPart.ContentLink = "http://www.sharepointdiary.com" 
                  #To set built-it properties, E.g. To set Set the Chrome type programmatically use:
                  $webPart.ChromeType = [System.Web.UI.WebControls.WebParts.PartChromeType]::None
                   
                  #Save the changes
                  $webPartManager.SaveChanges($webPart)
                  
                  write-host "Updated '$($OldTitle)' on $($web.URL)$($Page.ServerRelativeUrl)"
             }
         }
         $Page.CheckIn("Checkin by code")
         $Page.Publish("");
      }
  }


#Read more: http://www.sharepointdiary.com/2013/08/update-web-part-properties-programmatically.html#ixzz5aEuFJsag

