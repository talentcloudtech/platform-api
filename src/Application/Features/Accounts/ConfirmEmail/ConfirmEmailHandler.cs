using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Platform.Application.Services.Email;
using Platform.Domain.Entities;
using Platform.Domain.Interfaces;

namespace Platform.Application.Features.Accounts.ConfirmEmail;

/// <summary>
/// Handles the confirmation of an email address.
/// </summary>
public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequest, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmEmailHandler"/> class.
    /// </summary>
    /// <param name="userManager">The user manager service.</param>
    /// <param name="emailService">The email service.</param>
    public ConfirmEmailHandler(UserManager<ApplicationUser> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }

    /// <summary>
    /// Handles the confirmation of an email address.
    /// </summary>
    /// <param name="request">The request to confirm an email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the confirmation process.</returns>
    public async Task<Result> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result.Error("User not found");

        var result = await _userManager.ConfirmEmailAsync(user, request.Code);
        if (!result.Succeeded)
            return Result.Invalid(new ValidationError("Invalid confirmation code."));

        // Determine user type and send appropriate email
        var roles = await _userManager.GetRolesAsync(user);
        var isCustomer = roles.Contains("Customer");

        var emailContent = isCustomer
            ? BuildCustomerWelcomeEmail(user.UserName ?? "Valued Customer")
            : BuildUserWelcomeEmail(user.UserName ?? "Community Member");

        var mailRequest = new MailRequest(
            to: [user.Email!],
            subject: isCustomer ? "TalentCloud Welcomes You: Unlocking Access to Top Talent" : "Welcome to TalentCloud!",
            body: emailContent
        );

        await _emailService.SendEmailAsync(mailRequest);

        return Result.Success();
    }

    private static string BuildUserWelcomeEmail(string userName)
    {
        var message = new StringBuilder();
        message.AppendLine("<html>");
        message.AppendLine("<head>");
        message.AppendLine("<style>");
        message.AppendLine("body { font-family: Arial, sans-serif; line-height: 1.6; }");
        message.AppendLine("h2 { color: #4CAF50; }");
        message.AppendLine("p { font-size: 16px; }");
        message.AppendLine("ul { font-size: 16px; margin-top: 0; padding-left: 20px; }");
        message.AppendLine("</style>");
        message.AppendLine("</head>");
        message.AppendLine("<body>");
        message.AppendLine("<h2>A Warm Welcome to TalentCloud!</h2>");
        message.AppendLine($"<p>Dear {userName},</p>");
        message.AppendLine(
            "<p>Welcome to TalentCloud! We are thrilled to have you join our vibrant and talented community. TalentCloud is a platform that connects ambitious individuals with like-minded professionals, offering a space to collaborate, learn, and grow together. This is the first and only platform built 'For Community and By Community' and the founding team are former/ current community members and world champions.</p>"
        );
        message.AppendLine(
            "<p>As a new member, you are now part of a diverse network of talented individuals from various geographies, skills, industries and backgrounds. Our community is built on the principles of bringing the best of talent to the world in a fair and transparent platform, mutual support for launching your own ideas, knowledge-sharing in accelerating world of AI, and empowerment. We believe that together, we can achieve great things and make a positive impact.</p>"
        );
        message.AppendLine("<p>Our platform is based on three key technology pillars:</p>");
        message.AppendLine("<ul>");
        message.AppendLine("<li>AI based matching and validation of skills.</li>");
        message.AppendLine("<li>Developer productivity in an outcome focussed world.</li>");
        message.AppendLine("<li>AI acceleration and support for you to get more work done.</li>");
        message.AppendLine("</ul>");
        message.AppendLine("<p>Here's a glimpse of what you can expect as a TalentCloud community member:</p>");
        message.AppendLine("<ul>");
        message.AppendLine("<li>Opportunities: To grow, earn and contribute and most important have fun.</li>");
        message.AppendLine(
            "<li>Skill Development: Access a wealth of resources, workshops, and webinars to enhance your skills and stay updated with industry trends.</li>"
        );
        message.AppendLine(
            "<li>Collaborative Projects: Engage in exciting projects and initiatives, working alongside talented individuals to create innovative solutions.</li>"
        );
        message.AppendLine("<li>Mentorship and Guidance: Seek mentorship from experienced members and offer your expertise to those who need it.</li>");
        message.AppendLine(
            "<li>Community Events: Participate in virtual and in-person events, webinars, and social gatherings to build connections and have fun.</li>"
        );
        message.AppendLine(
            "<li>Networking: Connect with professionals across different fields, fostering valuable relationships and expanding your network.</li>"
        );
        message.AppendLine("</ul>");
        message.AppendLine("<p>To get started, we encourage you to:</p>");
        message.AppendLine("<ul>");
        message.AppendLine(
            "<li>Complete your profile: Ensure your TalentCloud profile is up-to-date and showcases your skills, experience, and areas of interest. This will help you connect with relevant opportunities and like-minded individuals.</li>"
        );
        message.AppendLine(
            "<li>Explore the Community Hub: Navigate through our community hub to discover ongoing discussions, resources, and events. Feel free to join conversations, share your insights, and engage with others.</li>"
        );
        message.AppendLine(
            "<li>Attend Welcome Sessions: Keep an eye out for our welcome sessions, where you can meet the team, ask questions, and learn more about TalentCloud's vision and community guidelines.</li>"
        );
        message.AppendLine("</ul>");
        message.AppendLine(
            "<p>Remember, TalentCloud is what you make of it. The more actively you engage, the more valuable connections and experiences you'll gain. We are here to support your journey and help you unlock your full potential.</p>"
        );
        message.AppendLine(
            "<p>If you have any questions or need assistance, please don't hesitate to reach out to our community managers. They are dedicated to ensuring a smooth onboarding experience and are always happy to help.</p>"
        );
        message.AppendLine(
            "<p>Once again, welcome to the TalentCloud family! We look forward to witnessing your growth and contributions within our community.</p>"
        );
        message.AppendLine("<p>Best regards,</p>");
        message.AppendLine("<p><strong>Minal Kanungo</strong><br>Founder, TalentCloud</p>");
        message.AppendLine("</body>");
        message.AppendLine("</html>");
        return message.ToString();
    }

    private static string BuildCustomerWelcomeEmail(string userName)
    {
        var message = new StringBuilder();
        StringBuilder sb = new StringBuilder();

        message.AppendLine("<html>");
        message.AppendLine("<head>");
        message.AppendLine("<style>");
        message.AppendLine("body { font-family: Arial, sans-serif; line-height: 1.6; }");
        message.AppendLine("h2 { color: #4CAF50; }");
        message.AppendLine("p { font-size: 16px; }");
        message.AppendLine("ul { font-size: 16px; margin-top: 0; padding-left: 20px; }");
        message.AppendLine("</style>");
        message.AppendLine("</head>");
        message.AppendLine("<body>");
        message.AppendLine("<h2>TalentCloud Welcomes You: Unlocking Access to Top Talent</h2>");
        message.AppendLine($"<p>Dear {userName},</p>");
        message.AppendLine(
            "<p>Congratulations on becoming a valued member of the TalentCloud community! We are excited to have you on board and are confident that our platform will revolutionize the way you find and engage with top-tier talent.</p>"
        );
        message.AppendLine(
            "<p>TalentCloud is a leading community platform designed to connect businesses like yours with exceptional designers, developers, and skilled resources from around the globe. Our mission is to streamline the talent acquisition process, ensuring you can focus on what matters most - building your business and bringing your vision to life.</p>"
        );
        message.AppendLine("<p>We are focussed towards solving three key problems for enterprises:</p>");
        message.AppendLine("<ul>");
        message.AppendLine(
            "<li><strong>Artificial Intelligence based Validated and Verified Talent Pool:</strong> Browse through a diverse range of talented individuals, carefully selected and vetted by our team. Find the perfect match for your specific project needs.</li>"
        );
        message.AppendLine(
            "<li><strong>Developer Productivity:</strong> One of major challenges for enterprises is to measure correctly the productivity of a developer. We are working to provide machine learning-based insights comparing your developer to the best in class in terms of productivity and outcome.</li>"
        );
        message.AppendLine(
            "<li><strong>Artificial Intelligence based Developer Acceleration:</strong> We have built-in tools that combine Human + AI to give you higher productivity, leveraging AI tech available today.</li>"
        );
        message.AppendLine("</ul>");
        message.AppendLine(
            "<p>As a TalentCloud customer, you gain access to an extensive network of highly skilled professionals who are ready to collaborate and contribute to your projects. Here's a glimpse of what you can expect:</p>"
        );
        message.AppendLine("<ul>");
        message.AppendLine(
            "<li><strong>Curated Talent Pool:</strong> Browse through a diverse range of talented individuals, carefully selected and vetted by our team. Find the perfect match for your specific project needs.</li>"
        );
        message.AppendLine(
            "<li><strong>Efficient Hiring Process:</strong> Our platform simplifies the hiring process, allowing you to connect, interview, and onboard talent seamlessly. Save time and resources by leveraging our efficient system.</li>"
        );
        message.AppendLine(
            "<li><strong>Project Management Tools:</strong> Manage your projects effectively with our integrated project management features. Collaborate, track progress, and ensure successful outcomes.</li>"
        );
        message.AppendLine(
            "<li><strong>Expertise and Support:</strong> Access a wealth of industry insights and support from our community. Tap into the collective knowledge of experienced professionals and seek guidance whenever needed.</li>"
        );
        message.AppendLine(
            "<li><strong>Flexible Engagement Models:</strong> Choose from various engagement models, including freelance, contract, or full-time positions, to align with your business requirements and budget.</li>"
        );
        message.AppendLine("</ul>");
        message.AppendLine("<p>To make the most of your TalentCloud experience, we recommend the following:</p>");
        message.AppendLine("<ul>");
        message.AppendLine(
            "<li><strong>Define Your Requirements:</strong> Clearly articulate your project goals, skill sets required, and any specific qualifications. This will help us match you with the most suitable talent.</li>"
        );
        message.AppendLine(
            "<li><strong>Explore Profiles:</strong> Browse through our extensive talent database and connect with individuals who align with your vision. Feel free to reach out and discuss potential collaborations.</li>"
        );
        message.AppendLine(
            "<li><strong>Utilize Our Resources:</strong> Take advantage of our comprehensive resources, including industry reports, case studies, and best practices, to enhance your talent acquisition strategy.</li>"
        );
        message.AppendLine(
            "<li><strong>Engage with the Community:</strong> Participate in community discussions, share your experiences, and build relationships with other like-minded businesses. Collaboration is key to success!</li>"
        );
        message.AppendLine("</ul>");
        message.AppendLine(
            "<p>Our team is dedicated to ensuring your satisfaction and providing an exceptional talent acquisition journey. If you have any questions or need assistance, our customer success managers are just a click away.</p>"
        );
        message.AppendLine(
            "<p>Welcome once again to the TalentCloud family! We are thrilled to be a part of your growth and success, and we look forward to witnessing the incredible projects you will bring to life.</p>"
        );
        message.AppendLine("<p>Best regards,</p>");
        message.AppendLine("<p><strong>Minal Kanungo</strong><br>Founder, TalentCloud</p>");
        message.AppendLine("</body>");
        message.AppendLine("</html>");
        return message.ToString();
    }
}
