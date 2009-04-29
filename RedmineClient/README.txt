h1. Redmine Client

h2. Installation

* Unzip the archive
* Edit *RedmineClient.exe.config*
* Run *RedmineClient.exe*

h2. Configuring Redmine to work properly with the Client

* Go to *Administration* -> *Settings*
* On *General* tab add *_999999_* to *Objects per page options* so that the value is for example _"25, 50, 100, 999999"_
* On *General* tab set *Feed content limit* to some reasonable number so that you see all your issues in the Redmine Client. If you for example have 100 issues in your project, you have to set it to a value bigger than 100 obviously.