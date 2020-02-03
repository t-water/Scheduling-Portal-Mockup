[Mockup Viewable Here](https://schedulingportalmockup.azurewebsites.net/)

#### While at the University of Southern California, I worked for Trojan Event Services as part of the House Staff. Considering that our shifts depended on when the events were scheduled, every week brought an entirely different schedule than the last as far as the number of shifts and the times they were occurring at. Although I loved the work environment in which no two shifts were the same, our website was simply not up for the task of handling such a dynamic scheduling challenge. Some of the most noteworthy issues were: 

* There was no way to find a sub for a shift within the website. It took a chain of 5 emails to find a sub and to get the shift change approved by an Executive House Manager.

* All time off requests were done via email, and if you sent an email on a friday, there was a good chance your email was going to get lost under all of the emails that were sent over the weekend.

* There were regular issues with people who had executive privileges not being able to access the parts of the website they needed to access.

* The website was functional on mobile, but was in no way optimized for mobile browsers. 

* The website was layed out using tables (gasp!).

#### Although I did not have the necessary skills and experience at the time to propose a new website, the challenges our website presented inspired me to create a mockup of what my ideal scheduling portal would look like. I accomplished this goal in the following ways:

* Cut the amount of emails that need to be exchanged by a significant portion. I was never an Executive House Manager, but I can only imagine how aggravating it must have been to field inquiries concerning shift swaps, time off, availability, and general questions on an email account that was shared by 5 people. My proposal moves shift swaps off of email and onto the scheduling website and takes a process that used to take 5+ emails and reduced it to just 3 clicks, all while still keeping in the loop all of the necessary parties. It also moves time off requests onto the website, freeing up email to be used for more serious inquiries. This seperation of concerns now avoids the problem of emails getting lost in the jumble that was the executive hm email account.

* Strictly define the distinctions between different types of users with regards to authorization. Utilizing ASP.NET Identity, roles-based authorization and claims-based authorization allows for a clear deliniation of who can access which endpoints, and what information they are able to view on every page. I cannot tell you how many times an email was sent out asking if people were still having trouble accessing things they needed to access. I wanted to create a website where once a user was promoted to a new position, they had access to all of the website privileges involved in that position, without having to go up the chain of command every time someone received a promotion.

* Have the website be as mobile-friendly as possible. Bootstrap's grid layout allows for the responsive nav-bar, tables, and forms present throughout the website. Although the actual schedule creation process is something that is more suited for a larger screen, or even two screens, most of the visits to the website would be college-aged employees looking to quickly check their schedule or request time off. These are individuals who have been browsing the internet on mobile devices for most of their lives, and they will be expecting a website that looks good and performs well on mobile.

(This website is in no way affiliated with Trojan Event Services, and any references to Trojan Event Services or TE Server are for stylistic purposes only)
