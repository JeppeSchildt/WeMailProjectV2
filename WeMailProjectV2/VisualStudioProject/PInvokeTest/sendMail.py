import smtplib
host = "mail.smtp2go.com" 
server = smtplib.SMTP(host)
FROM = "Whatever@DontMatter.com"
TO = "Schildt0606@Gmai.com"
SUBJECT = "Indkraevning af rest-skat"
TEXT = "Hej Jacob, Du skylder skat 2000,- DKK, venligst overfoer hurtigst muligt til 31352037 paa mobilepay"
MSG = 'Subject: {}\n\n{}'.format(SUBJECT, TEXT)
server.sendmail(FROM, TO, MSG)

server.quit()
print ("Email Send")
