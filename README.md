# IpAddressViaMail
Auto send ip address via email (SMTP). The email can be set to send when the program is start or when the ip address is changed.

## Configure

To Config this Program, you have to reanme the config.json.sample to config.json and fill in the files. Here is a sample of the config file:

```
{
	"FromMailAddress":"test@163.com",
	"ToMailAddresses":["test@163.com","test@gmail.com"],
	"SmtpServer":"smtp.163.com",
	"SmtpUsername":"test@163.com",
	"SmtpPassword":"password",
	"BanLocalIp":true,
	"SendAtStartUp":true,
	"SendWhenChange":true
}
```
