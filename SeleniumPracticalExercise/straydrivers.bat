rem Kills Selenium driver instances

taskkill /f /im chromedriver.exe 2>nul
taskkill /f /im msedgedriver.exe 2>nul

exit /b 0