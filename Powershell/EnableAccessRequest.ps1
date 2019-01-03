Add-PSSnapin Microsoft.SharePoint.Powershell
 
#Set the site URL variable accordingly!
$SiteURL = "http://sp-apps1:8080"
 
$site = Get-SPSite $SiteURL
 
    ForEach ($web in $site.AllWebs | where { $_.Permissions.Inherited -eq $false})
          {
                #sharepoint 2013 access request settings powershell
                $web.RequestAccessEmail="firdaus.nasir@utp.edu.my"
                write-host Updated Access request settings for $web.Title, at: $web.URL
         }


#Read more: http://www.sharepointdiary.com/2014/11/manage-access-request-settings-in-sharepoint-2013.html#ixzz5a7tCHLO7