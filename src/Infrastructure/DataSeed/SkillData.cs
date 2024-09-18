using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Platform.Domain.Entities;

namespace Platform.Infrastructure.DataSeed;

/// <summary>
/// Skill data seed.
/// </summary>
public static class SkillData
{
    /// <summary>
    /// Seeds the skill data.
    /// </summary>
    /// <param name="context">The application database context.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>the task completed</returns>
    public static async Task SeedAsync(ApplicationDbContext context, ILogger logger, CancellationToken cancellationToken)
    {
        var skills = GetSkills();
        var existingSkillIds = await context.Skills.Select(s => s.Id).ToListAsync(cancellationToken);

        var newSkills = skills.Where(s => !existingSkillIds.Contains(s.Id)).ToList();

        if (newSkills.Any())
        {
            await context.Skills.AddRangeAsync(newSkills, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Role data seeding completed successfully.");
        }
    }

    private static List<Skill> GetSkills()
    {
        return new List<Skill>
        {
            new Skill
            {
                Id = "xnvmp2dpz97dxapqton47",
                Name = ".NET",
                Key = "dotnet",
                Path = "/assets/skills/dotnet.svg",
                Keywords = new[]
                {
                    "dotnet",
                    ".NET",
                    "C#",
                    "Microsoft",
                    "Windows",
                    "Desktop Applications",
                    "Web Development",
                    "Backend",
                    "Entity Framework",
                    "LINQ",
                    "WPF",
                    "WinForms",
                },
            },
            new Skill
            {
                Id = "vizu8uknqukgeaaoi99mx",
                Name = ".NET Core",
                Key = "dotnet-core",
                Path = "/assets/skills/dotnet-core.svg",
                Keywords = new[]
                {
                    "dotnet",
                    ".NET Core",
                    "C#",
                    "Microsoft",
                    "Backend Development",
                    "Web API",
                    "Microservices",
                    "ASP.NET Core",
                    "Entity Framework",
                    "MVC",
                    "RESTful API",
                    "Blazor",
                    "SignalR",
                },
            },
            new Skill
            {
                Id = "pvscz8t37hjphi25m8nqs",
                Name = "AArch64",
                Key = "aarch64",
                Path = "/assets/skills/AArch64.svg",
                Keywords = new[] { "AArch64", "ARM Architecture", "Embedded Systems", "Low-Level Programming", "Assembler", "IoT" },
            },
            new Skill
            {
                Id = "67gcx33vz7s2m6jafxg6g",
                Name = "Adobe Commerce (Magento)",
                Key = "adobe-commerce-magento",
                Path = "/assets/skills/Adobe-Commerce-(Magneto).svg",
                Keywords = new[]
                {
                    "Adobe Commerce",
                    "Magento",
                    "E-commerce",
                    "Online Store",
                    "PHP",
                    "Open Source",
                    "Shopping Cart",
                    "Magento 2",
                    "Payment Gateway",
                    "Extensions",
                },
            },
            new Skill
            {
                Id = "rer1edgidxr8kxfxakmka",
                Name = "Adobe Illustrator",
                Key = "adobe-illustrator",
                Path = "/assets/skills/Adobe-Illustrator.svg",
                Keywords = new[]
                {
                    "Adobe Illustrator",
                    "Graphic Design",
                    "Vector Graphics",
                    "Logo Design",
                    "Illustration",
                    "Branding",
                    "Typography",
                    "Creative Suite",
                },
            },
            new Skill
            {
                Id = "hn25oxxhdz1qv9wwjzzpc",
                Name = "Adobe Photoshop",
                Key = "adobe-photoshop",
                Path = "/assets/skills/Adobe-Photoshop.svg",
                Keywords = new[]
                {
                    "Adobe Photoshop",
                    "Photo Editing",
                    "Graphic Design",
                    "Image Manipulation",
                    "Digital Art",
                    "Retouching",
                    "Creative Suite",
                    "Layers",
                    "Filters",
                },
            },
            new Skill
            {
                Id = "q2aengwfzusywgbjo429c",
                Name = "Adobe Premiere Pro",
                Key = "adobe-premiere-pro",
                Path = "/assets/skills/Adobe-Premiere-Pro.svg",
                Keywords = new[]
                {
                    "Adobe Premiere Pro",
                    "Video Editing",
                    "Motion Graphics",
                    "Video Production",
                    "Film Editing",
                    "Adobe Creative Suite",
                    "Timeline",
                    "Color Correction",
                    "Post-Production",
                },
            },
            new Skill
            {
                Id = "4ys8t8es4i4z4y182pjrs",
                Name = "Adobe XD",
                Key = "adobe-xd",
                Path = "/assets/skills/Adobe-XD.svg",
                Keywords = new[]
                {
                    "Adobe XD",
                    "UI/UX Design",
                    "Prototyping",
                    "Wireframing",
                    "User Interface",
                    "User Experience",
                    "Design Systems",
                    "Interactive Prototypes",
                    "Vector Design",
                },
            },
            new Skill
            {
                Id = "uqr8133gadvz7tfivxoic",
                Name = "AdonisJS",
                Key = "adonisjs",
                Path = "/assets/skills/AdonisJS.svg",
                Keywords = new[]
                {
                    "AdonisJS",
                    "JavaScript",
                    "Node.js",
                    "MVC Framework",
                    "Backend Development",
                    "RESTful API",
                    "ORM",
                    "WebSockets",
                    "Middleware",
                },
            },
            new Skill
            {
                Id = "3jm4perf231n8uy7j6b9w",
                Name = "After Effects",
                Key = "after-effects",
                Path = "/assets/skills/After-Effects.svg",
                Keywords = new[]
                {
                    "After Effects",
                    "Motion Graphics",
                    "Visual Effects",
                    "Video Editing",
                    "Animation",
                    "Post-Production",
                    "Adobe",
                    "Compositing",
                    "Video Effects",
                },
            },
            new Skill
            {
                Id = "6dfq7ftmqh1b6tb7vm1x6",
                Name = "Akka",
                Key = "akka",
                Path = "/assets/skills/Akka.svg",
                Keywords = new[]
                {
                    "Akka",
                    "Scala",
                    "Distributed Systems",
                    "Concurrency",
                    "Actor Model",
                    "Reactive Programming",
                    "Microservices",
                    "Message-Driven",
                    "Fault-Tolerance",
                },
            },
            new Skill
            {
                Id = "65exc82v3svxzqsq5f8n4",
                Name = "Algolia",
                Key = "algolia",
                Path = "/assets/skills/Algolia.svg",
                Keywords = new[]
                {
                    "Algolia",
                    "Search Engine",
                    "Full-Text Search",
                    "Real-Time Search",
                    "API",
                    "Search-as-a-Service",
                    "Instant Search",
                    "Autocomplete",
                    "Faceted Search",
                },
            },
            new Skill
            {
                Id = "cenk2j6uauda7vzik8x8p",
                Name = "Alpine.js",
                Key = "alpine-js",
                Path = "/assets/skills/Alpine.js.svg",
                Keywords = new[]
                {
                    "Alpine.js",
                    "JavaScript",
                    "Frontend Development",
                    "Lightweight Framework",
                    "Reactive Data",
                    "DOM Manipulation",
                    "Minimalistic",
                    "Tailwind CSS",
                },
            },
            new Skill
            {
                Id = "y99czcsg77ozcu7zq65di",
                Name = "Anaconda",
                Key = "anaconda",
                Path = "/assets/skills/Anaconda.svg",
                Keywords = new[]
                {
                    "Anaconda",
                    "Python",
                    "Data Science",
                    "Machine Learning",
                    "Data Analysis",
                    "Jupyter Notebooks",
                    "Pandas",
                    "Numpy",
                    "SciPy",
                    "Conda",
                },
            },
            new Skill
            {
                Id = "utzyjzcs16nq7tjtovkvc",
                Name = "Android",
                Key = "android",
                Path = "/assets/skills/Android.svg",
                Keywords = new[]
                {
                    "Android",
                    "Mobile Development",
                    "Java",
                    "Kotlin",
                    "Google",
                    "Open Source",
                    "App Development",
                    "Mobile Apps",
                    "Smartphones",
                },
            },
            new Skill
            {
                Id = "ouk23opmjkxs4nwate394",
                Name = "Android Studio",
                Key = "android-studio",
                Path = "/assets/skills/Android-Studio.svg",
                Keywords = new[] { "Android Studio", "Android Development", "Kotlin", "Java", "Mobile Apps", "IDE", "Gradle", "SDK", "Emulator", "UI Design" },
            },
            new Skill
            {
                Id = "s5tvfkxp3uwopdj7njaba",
                Name = "Angular",
                Key = "angular",
                Path = "/assets/skills/Angular.svg",
                Keywords = new[]
                {
                    "Angular",
                    "JavaScript",
                    "TypeScript",
                    "Frontend Development",
                    "Web Applications",
                    "Single-Page Applications",
                    "SPAs",
                    "Google",
                    "MVC",
                    "Reactive Programming",
                    "Components",
                    "Directives",
                    "RxJS",
                },
            },
            new Skill
            {
                Id = "kyukkan63za6w9dhgy19o",
                Name = "AngularJS",
                Key = "angularjs",
                Path = "/assets/skills/AngularJS.svg",
                Keywords = new[]
                {
                    "AngularJS",
                    "JavaScript",
                    "Frontend Development",
                    "Web Applications",
                    "Single-Page Applications",
                    "SPAs",
                    "MVC",
                    "Directives",
                    "Data Binding",
                },
            },
            new Skill
            {
                Id = "iufuhe19sry5akwqz318m",
                Name = "Ansible",
                Key = "ansible",
                Path = "/assets/skills/Ansible.svg",
                Keywords = new[]
                {
                    "Ansible",
                    "Automation",
                    "DevOps",
                    "Infrastructure as Code",
                    "IaC",
                    "Configuration Management",
                    "IT Automation",
                    "Orchestration",
                    "Red Hat",
                    "Playbooks",
                    "YAML",
                },
            },
            new Skill
            {
                Id = "jxsupattkixke8nb5tdqc",
                Name = "Ant Design",
                Key = "ant-design",
                Path = "/assets/skills/Ant-Design.svg",
                Keywords = new[]
                {
                    "Ant Design",
                    "UI Framework",
                    "React",
                    "Frontend Development",
                    "Design System",
                    "Component Library",
                    "Responsive Design",
                    "CSS",
                    "Web Development",
                },
            },
            new Skill
            {
                Id = "iab9xtsq4uvrh42y5x6i4",
                Name = "Apache",
                Key = "apache",
                Path = "/assets/skills/Apache.svg",
                Keywords = new[]
                {
                    "Apache",
                    "Web Server",
                    "HTTP Server",
                    "Open Source",
                    "Linux",
                    "UNIX",
                    "LAMP Stack",
                    "Reverse Proxy",
                    "SSL",
                    "Load Balancing",
                },
            },
            new Skill
            {
                Id = "bmdq7chviokdo4i93nh8o",
                Name = "Apache Airflow",
                Key = "apache-airflow",
                Path = "/assets/skills/Apache-Airflow.svg",
                Keywords = new[]
                {
                    "Apache Airflow",
                    "Workflow Automation",
                    "Data Pipelines",
                    "ETL",
                    "Data Engineering",
                    "Orchestration",
                    "Task Scheduling",
                    "Python",
                    "DAGs",
                },
            },
            new Skill
            {
                Id = "gk145s5nokcknb7qiyq61",
                Name = "Apache Cassandra",
                Key = "apache-cassandra",
                Path = "/assets/skills/Apache-Cassandra.svg",
                Keywords = new[]
                {
                    "Apache Cassandra",
                    "NoSQL Database",
                    "Distributed Database",
                    "Big Data",
                    "Scalability",
                    "High Availability",
                    "Partitioning",
                    "Replication",
                    "Data Model",
                    "Query Language",
                },
            },
            new Skill
            {
                Id = "yswydnz7sokwp71pes9ew",
                Name = "Apache Groovy",
                Key = "apache-groovy",
                Path = "/assets/skills/Apache-Groovy.svg",
                Keywords = new[] { "Apache Groovy", "Java", "Scripting Language", "Dynamic Language", "JVM", "DSL", "Grails", "Build Automation" },
            },
            new Skill
            {
                Id = "1iietvunmih4vihiaqsts",
                Name = "Apache Hadoop",
                Key = "apache-hadoop",
                Path = "/assets/skills/Apache-Hadoop.svg",
                Keywords = new[]
                {
                    "Apache Hadoop",
                    "Big Data",
                    "Distributed Computing",
                    "MapReduce",
                    "HDFS",
                    "YARN",
                    "Data Processing",
                    "Cloudera",
                    "Hortonworks",
                    "Apache",
                },
            },
            new Skill
            {
                Id = "fnhffinhfcatdfn2oh31q",
                Name = "Apache Kafka",
                Key = "apache-kafka",
                Path = "/assets/skills/Apache-Kafka.svg",
                Keywords = new[]
                {
                    "Apache Kafka",
                    "Messaging System",
                    "Stream Processing",
                    "Distributed Systems",
                    "Real-Time Data",
                    "Event-Driven Architecture",
                    "Microservices",
                    "Publish-Subscribe",
                    "Zookeeper",
                },
            },
            new Skill
            {
                Id = "4c6jtg3xu6xz9eywgya29",
                Name = "Apache Maven",
                Key = "apache-maven",
                Path = "/assets/skills/Apache-Maven.svg",
                Keywords = new[]
                {
                    "Apache Maven",
                    "Build Automation",
                    "Java",
                    "Project Management",
                    "Dependency Management",
                    "Plugins",
                    "Lifecycle",
                    "POM",
                    "Continuous Integration",
                    "CI/CD",
                },
            },
            new Skill
            {
                Id = "8npbxiwoskzoim7advqst",
                Name = "Apache Spark",
                Key = "apache-spark",
                Path = "/assets/skills/Apache-Spark.svg",
                Keywords = new[]
                {
                    "Apache Spark",
                    "Big Data",
                    "Distributed Computing",
                    "Data Processing",
                    "In-Memory Computing",
                    "MapReduce",
                    "Machine Learning",
                    "SQL",
                    "Streaming",
                },
            },
            new Skill
            {
                Id = "awww6uyojsaqvjzd2m4jb",
                Name = "Apache Subversion",
                Key = "apache-subversion",
                Path = "/assets/skills/Apache-Subversion.svg",
                Keywords = new[]
                {
                    "Apache Subversion",
                    "SVN",
                    "Version Control",
                    "Source Code Management",
                    "SCM",
                    "Commit",
                    "Branching",
                    "Merging",
                    "Repository",
                },
            },
            new Skill
            {
                Id = "sz9e4jqqj8qaoaadgsako",
                Name = "Apache Tomcat",
                Key = "apache-tomcat",
                Path = "/assets/skills/Apache-Tomcat.svg",
                Keywords = new[] { "Apache Tomcat", "Java", "Web Server", "Servlet Container", "JSP", "Web Applications", "Open Source", "HTTP", "Apache" },
            },
            new Skill
            {
                Id = "4v3f5ajkoxegtik5yq6u8",
                Name = "APL",
                Key = "apl",
                Path = "/assets/skills/APL.svg",
                Keywords = new[] { "APL", "Array Programming", "Mathematics", "Scientific Computing", "Data Analysis", "Algorithm Design" },
            },
            new Skill
            {
                Id = "vzvbxyr9rsebtod8jdwwh",
                Name = "Appcelerator",
                Key = "appcelerator",
                Path = "/assets/skills/Appcelerator.svg",
                Keywords = new[]
                {
                    "Appcelerator",
                    "Mobile Development",
                    "JavaScript",
                    "Cross-Platform",
                    "Titanium SDK",
                    "Mobile Apps",
                    "Native Apps",
                    "Android",
                    "iOS",
                    "Cloud Services",
                },
            },
            new Skill
            {
                Id = "9c3f73umt8oaa8tacjpje",
                Name = "Apple",
                Key = "apple",
                Path = "/assets/skills/Apple.svg",
                Keywords = new[] { "Apple", "MacOS", "iOS", "Hardware", "Software", "User Experience", "Design", "Innovation", "Ecosystem", "Mobile" },
            },
            new Skill
            {
                Id = "6j4zvub88xx8uqc5h4h1x",
                Name = "Apple Safari",
                Key = "apple-safari",
                Path = "/assets/skills/Apple-Safari.svg",
                Keywords = new[] { "Apple Safari", "Web Browser", "Apple", "MacOS", "iOS", "WebKit", "Browsing", "Internet" },
            },
            new Skill
            {
                Id = "zipmdkqzr9be1vsytngkd",
                Name = "Appwrite",
                Key = "appwrite",
                Path = "/assets/skills/Appwrite.svg",
                Keywords = new[]
                {
                    "Appwrite",
                    "Backend as a Service",
                    "BaaS",
                    "Serverless",
                    "API",
                    "Authentication",
                    "Realtime",
                    "Database",
                    "File Storage",
                    "Self-Hosted",
                },
            },
            new Skill
            {
                Id = "ctz6cjoiqzs19dox32x16",
                Name = "Arch Linux",
                Key = "arch-linux",
                Path = "/assets/skills/Arch-Linux.svg",
                Keywords = new[]
                {
                    "Arch Linux",
                    "Linux",
                    "Operating System",
                    "Rolling Release",
                    "Customization",
                    "Pacman",
                    "Open Source",
                    "Lightweight",
                    "Unix-Like",
                },
            },
            new Skill
            {
                Id = "sui1ksixodntawekxerao",
                Name = "Arduino",
                Key = "arduino",
                Path = "/assets/skills/Arduino.svg",
                Keywords = new[] { "Arduino", "Microcontroller", "Electronics", "Embedded Systems", "Prototyping", "DIY", "IoT", "C/C++", "Hardware" },
            },
            new Skill
            {
                Id = "1tt9ypbz6c77tey96grvj",
                Name = "Argo CD",
                Key = "argo-cd",
                Path = "/assets/skills/Argo-CD.svg",
                Keywords = new[] { "Argo CD", "GitOps", "Continuous Delivery", "Kubernetes", "CI/CD", "DevOps", "Automation", "Version Control", "Helm" },
            },
            new Skill
            {
                Id = "ov11bmtgpbcvovkqsc1xh",
                Name = "Astro",
                Key = "astro",
                Path = "/assets/skills/Astro.svg",
                Keywords = new[]
                {
                    "Astro",
                    "Static Site Generator",
                    "JavaScript",
                    "Frontend Development",
                    "Web Development",
                    "React",
                    "Vue",
                    "Svelte",
                    "Markdown",
                },
            },
            new Skill
            {
                Id = "u13w9mdxyuy53wwj24e9i",
                Name = "Atom",
                Key = "atom",
                Path = "/assets/skills/Atom.svg",
                Keywords = new[] { "Atom", "Text Editor", "Open Source", "GitHub", "IDE", "Customization", "Packages", "Electron", "Coding" },
            },
            new Skill
            {
                Id = "cxz2w7t1ji1an4qfqzuwm",
                Name = "Autodesk Maya",
                Key = "autodesk-maya",
                Path = "/assets/skills/Autodesk-Maya.svg",
                Keywords = new[]
                {
                    "Autodesk Maya",
                    "3D Modeling",
                    "Animation",
                    "Rendering",
                    "Visual Effects",
                    "VFX",
                    "3D Graphics",
                    "CGI",
                    "Character Animation",
                    "Rigging",
                },
            },
            new Skill
            {
                Id = "9we3vtx5h88iabynuvqfc",
                Name = "Autodesk ShotGrid",
                Key = "autodesk-shotgrid",
                Path = "/assets/skills/Autodesk-ShotGrid.svg",
                Keywords = new[]
                {
                    "Autodesk ShotGrid",
                    "Project Management",
                    "Pipeline Management",
                    "VFX",
                    "CGI",
                    "Animation",
                    "Shotgun",
                    "Collaboration",
                    "Scheduling",
                },
            },
            new Skill
            {
                Id = "6k3c2oi8ipmiiq918u84y",
                Name = "Awk",
                Key = "awk",
                Path = "/assets/skills/Awk.svg",
                Keywords = new[]
                {
                    "Awk",
                    "Text Processing",
                    "Scripting Language",
                    "UNIX",
                    "Command Line",
                    "Pattern Matching",
                    "Data Extraction",
                    "Automation",
                },
            },
            new Skill
            {
                Id = "373yfx3vz4o1vykvvh39a",
                Name = "AWS",
                Key = "aws",
                Path = "/assets/skills/AWS.svg",
                Keywords = new[]
                {
                    "AWS",
                    "Cloud Computing",
                    "Amazon Web Services",
                    "Infrastructure as a Service",
                    "IaaS",
                    "Serverless",
                    "Lambda",
                    "EC2",
                    "S3",
                    "CloudFormation",
                    "DevOps",
                    "Cloud Security",
                    "Networking",
                },
            },
            new Skill
            {
                Id = "h7pz1rsuxd43rb37eagka",
                Name = "Axios",
                Key = "axios",
                Path = "/assets/skills/Axios.svg",
                Keywords = new[]
                {
                    "Axios",
                    "JavaScript",
                    "HTTP Client",
                    "API Requests",
                    "AJAX",
                    "Promises",
                    "REST API",
                    "Asynchronous Programming",
                    "Frontend Development",
                },
            },
            new Skill
            {
                Id = "4dp7uz5mz6v3zudhnmucn",
                Name = "Azure",
                Key = "azure",
                Path = "/assets/skills/Azure.svg",
                Keywords = new[]
                {
                    "Azure",
                    "Cloud Computing",
                    "Microsoft",
                    "PaaS",
                    "IaaS",
                    "SaaS",
                    "DevOps",
                    "Cloud Services",
                    "Virtual Machines",
                    "Azure AD",
                    "Azure DevOps",
                },
            },
            new Skill
            {
                Id = "47b34xsosd4i2dw8vatq8",
                Name = "Azure DevOps",
                Key = "azure-devops",
                Path = "/assets/skills/Azure-Devops.svg",
                Keywords = new[]
                {
                    "Azure DevOps",
                    "CI/CD",
                    "DevOps",
                    "Microsoft",
                    "Continuous Integration",
                    "Continuous Delivery",
                    "Pipelines",
                    "Version Control",
                    "Azure",
                },
            },
            new Skill
            {
                Id = "b5cnqvqpzc57c38o2ebrt",
                Name = "Azure SQL Database",
                Key = "azure-sql-database",
                Path = "/assets/skills/Azure-SQL-Database.svg",
                Keywords = new[]
                {
                    "Azure SQL Database",
                    "SQL",
                    "Microsoft",
                    "Azure",
                    "Cloud Database",
                    "Data Management",
                    "Scalability",
                    "Security",
                    "Relational Database",
                },
            },
            new Skill
            {
                Id = "r35aezqf5cj7p5uhzrv1t",
                Name = "Babel",
                Key = "babel",
                Path = "/assets/skills/Babel.svg",
                Keywords = new[]
                {
                    "Babel",
                    "JavaScript",
                    "Transpiler",
                    "ES6",
                    "ESNext",
                    "Frontend Development",
                    "JavaScript Compiler",
                    "Build Tools",
                    "Web Development",
                },
            },
            new Skill
            {
                Id = "gftszj3gmowku5v6hs78w",
                Name = "Backbone.js",
                Key = "backbone-js",
                Path = "/assets/skills/Backbone.js.svg",
                Keywords = new[]
                {
                    "Backbone.js",
                    "JavaScript",
                    "Frontend Development",
                    "MVC Framework",
                    "Single-Page Applications",
                    "SPAs",
                    "Web Applications",
                    "Models",
                    "Views",
                    "Collections",
                },
            },
            new Skill
            {
                Id = "a8zrijdgwnyuwikwtr99b",
                Name = "Ballerina",
                Key = "ballerina",
                Path = "/assets/skills/Ballerina.svg",
                Keywords = new[]
                {
                    "Ballerina",
                    "Programming Language",
                    "Microservices",
                    "Cloud Native",
                    "RESTful API",
                    "Message Passing",
                    "Data Streams",
                    "Concurrency",
                },
            },
            new Skill
            {
                Id = "77ac7j5833bc1i978gc9t",
                Name = "Bamboo",
                Key = "bamboo",
                Path = "/assets/skills/Bamboo.svg",
                Keywords = new[]
                {
                    "Bamboo",
                    "CI/CD",
                    "Continuous Integration",
                    "Continuous Delivery",
                    "DevOps",
                    "Automation",
                    "Atlassian",
                    "Build Pipelines",
                    "Version Control",
                },
            },
            new Skill
            {
                Id = "fj23jk75mqk2pa7wp819h",
                Name = "Bash",
                Key = "bash",
                Path = "/assets/skills/Bash.svg",
                Keywords = new[]
                {
                    "Bash",
                    "Shell Scripting",
                    "Command Line",
                    "Linux",
                    "UNIX",
                    "Automation",
                    "Scripting Language",
                    "System Administration",
                    "Bourne Again Shell",
                },
            },
            new Skill
            {
                Id = "2rza6vmgeu2duq2yt3th7",
                Name = "Behance",
                Key = "behance",
                Path = "/assets/skills/Behance.svg",
                Keywords = new[] { "Behance", "Portfolio", "Creative Network", "Graphic Design", "Art", "Illustration", "Photography", "Showcase", "Creative" },
            },
            new Skill
            {
                Id = "a3na8tabbn74ka64ycao5",
                Name = "BitBucket",
                Key = "bitbucket",
                Path = "/assets/skills/BitBucket.svg",
                Keywords = new[] { "BitBucket", "Version Control", "Git", "Mercurial", "Atlassian", "Source Code Management", "SCM", "CI/CD", "DevOps" },
            },
            new Skill
            {
                Id = "vf7tp4ni4hszpevf1ts94",
                Name = "Blender",
                Key = "blender",
                Path = "/assets/skills/Blender.svg",
                Keywords = new[]
                {
                    "Blender",
                    "3D Modeling",
                    "Animation",
                    "Rendering",
                    "VFX",
                    "Open Source",
                    "CGI",
                    "Game Development",
                    "Sculpting",
                    "Visual Effects",
                },
            },
            new Skill
            {
                Id = "4tsw8euejynhtervtcknj",
                Name = "Bootstrap",
                Key = "bootstrap",
                Path = "/assets/skills/Bootstrap.svg",
                Keywords = new[]
                {
                    "Bootstrap",
                    "CSS Framework",
                    "Frontend Development",
                    "Responsive Design",
                    "Web Development",
                    "UI Components",
                    "HTML",
                    "JavaScript",
                    "Grid System",
                },
            },
            new Skill
            {
                Id = "rotdws3xc39xtdqncwuqg",
                Name = "Bower",
                Key = "bower",
                Path = "/assets/skills/Bower.svg",
                Keywords = new[]
                {
                    "Bower",
                    "Package Manager",
                    "Web Development",
                    "Frontend Development",
                    "Dependency Management",
                    "JavaScript",
                    "CSS",
                    "HTML",
                },
            },
            new Skill
            {
                Id = "h2ime464m19qincdixiqn",
                Name = "BrowserStack",
                Key = "browserstack",
                Path = "/assets/skills/BrowserStack.svg",
                Keywords = new[]
                {
                    "BrowserStack",
                    "Cross-Browser Testing",
                    "Web Development",
                    "Cloud Testing",
                    "Selenium",
                    "Mobile Testing",
                    "Responsive Design",
                },
            },
            new Skill
            {
                Id = "qhd5mzjm9e5ypffvq6m3s",
                Name = "Bulma",
                Key = "bulma",
                Path = "/assets/skills/Bulma.svg",
                Keywords = new[]
                {
                    "Bulma",
                    "CSS Framework",
                    "Frontend Development",
                    "Responsive Design",
                    "Web Development",
                    "UI Components",
                    "HTML",
                    "JavaScript",
                    "Flexbox",
                },
            },
            new Skill
            {
                Id = "jtw44tsasqkjec8tuv6vw",
                Name = "Bun",
                Key = "bun",
                Path = "/assets/skills/Bun.svg",
                Keywords = new[]
                {
                    "Bun",
                    "JavaScript",
                    "Runtime",
                    "Node.js",
                    "Server-Side",
                    "Web Development",
                    "Frontend Development",
                    "Bundler",
                    "NPM",
                    "Tooling",
                },
            },
            new Skill
            {
                Id = "adyf2ucsx1nb1zysbt7qd",
                Name = "C",
                Key = "c",
                Path = "/assets/skills/C.svg",
                Keywords = new[]
                {
                    "C",
                    "Programming Language",
                    "System Programming",
                    "Embedded Systems",
                    "Operating Systems",
                    "Low-Level Programming",
                    "ANSI C",
                    "Algorithms",
                    "Data Structures",
                },
            },
            new Skill
            {
                Id = "w2st1kz5py2psaitxsaor",
                Name = "C# (CSharp)",
                Key = "csharp",
                Path = "/assets/skills/csharp.svg",
                Keywords = new[]
                {
                    "C#",
                    "CSharp",
                    "Programming Language",
                    "Microsoft",
                    "Backend Development",
                    "Web Development",
                    "Desktop Applications",
                    "Game Development",
                    "Unity",
                },
            },
            new Skill
            {
                Id = "7g512i1a9jacysfe724eu",
                Name = "C++ (CPlusPlus)",
                Key = "cplusplus",
                Path = "/assets/skills/cplusplus.svg",
                Keywords = new[]
                {
                    "C++",
                    "CPlusPlus",
                    "Programming Language",
                    "System Programming",
                    "Game Development",
                    "High Performance",
                    "Embedded Systems",
                    "Object-Oriented Programming",
                    "OOP",
                },
            },
            new Skill
            {
                Id = "inv4jcsf69xcjkkrq7ggt",
                Name = "Cairo Graphics",
                Key = "cairo-graphics",
                Path = "/assets/skills/Cairo-Graphics.svg",
                Keywords = new[] { "Cairo Graphics", "2D Graphics", "Vector Graphics", "Rendering", "Open Source", "Drawing API", "Cross-Platform" },
            },
            new Skill
            {
                Id = "k9v8m7nc3gwj7kqx279a8",
                Name = "CakePHP",
                Key = "cakephp",
                Path = "/assets/skills/CakePHP.svg",
                Keywords = new[] { "CakePHP", "PHP", "Web Development", "MVC Framework", "ORM", "Backend Development", "Open Source", "Rapid Development" },
            },
            new Skill
            {
                Id = "2veom56o4j26ioxn5oceo",
                Name = "Canva",
                Key = "canva",
                Path = "/assets/skills/Canva.svg",
                Keywords = new[] { "Canva", "Graphic Design", "Online Design Tool", "Photo Editing", "Templates", "Social Media Graphics", "Marketing" },
            },
            new Skill
            {
                Id = "j1arb2yo1x8143815sggp",
                Name = "Capacitor",
                Key = "capacitor",
                Path = "/assets/skills/Capacitor.svg",
                Keywords = new[]
                {
                    "Capacitor",
                    "Cross-Platform",
                    "Mobile Development",
                    "Hybrid Apps",
                    "Web Apps",
                    "Native APIs",
                    "JavaScript",
                    "PWA",
                    "Ionic Framework",
                },
            },
            new Skill
            {
                Id = "rjve1ctma1ktcba41j3hy",
                Name = "CentOS",
                Key = "centos",
                Path = "/assets/skills/CentOS.svg",
                Keywords = new[] { "CentOS", "Linux", "Operating System", "Server", "Red Hat", "Enterprise Linux", "Open Source", "RPM" },
            },
            new Skill
            {
                Id = "o5d6edka6vp7njn9vr3hs",
                Name = "Chrome",
                Key = "chrome",
                Path = "/assets/skills/Chrome.svg",
                Keywords = new[] { "Chrome", "Web Browser", "Google", "Browsing", "Internet", "Web Development", "Extensions", "Open Source", "Chromium" },
            },
            new Skill
            {
                Id = "rsc786jxc593pp4rtosea",
                Name = "CircleCI",
                Key = "circleci",
                Path = "/assets/skills/CircleCI.svg",
                Keywords = new[]
                {
                    "CircleCI",
                    "CI/CD",
                    "Continuous Integration",
                    "Continuous Delivery",
                    "DevOps",
                    "Automation",
                    "Build Pipelines",
                    "Version Control",
                },
            },
            new Skill
            {
                Id = "fqx5b2eckkw69n2h7k5mn",
                Name = "Clarity",
                Key = "clarity",
                Path = "/assets/skills/Clarity.svg",
                Keywords = new[] { "Clarity", "UI Framework", "Frontend Development", "Design System", "Web Development", "Angular", "Components", "CSS" },
            },
            new Skill
            {
                Id = "9wq4zcohgzqccb2xjf7af",
                Name = "CLion",
                Key = "clion",
                Path = "/assets/skills/CLion.svg",
                Keywords = new[] { "CLion", "JetBrains", "IDE", "C++", "C", "Refactoring", "Code Analysis", "Debugger", "CMake", "Cross-Platform" },
            },
            new Skill
            {
                Id = "pp1xx8az4vwkynttyuoth",
                Name = "Clojure",
                Key = "clojure",
                Path = "/assets/skills/Clojure.svg",
                Keywords = new[]
                {
                    "Clojure",
                    "Lisp",
                    "Functional Programming",
                    "JVM",
                    "Concurrency",
                    "Immutability",
                    "Data Structures",
                    "REPL",
                    "ClojureScript",
                },
            },
            new Skill
            {
                Id = "ybb29vngybtmrrfepwxrg",
                Name = "ClojureScript",
                Key = "clojurescript",
                Path = "/assets/skills/ClojureScript.svg",
                Keywords = new[]
                {
                    "ClojureScript",
                    "Clojure",
                    "JavaScript",
                    "Frontend Development",
                    "Functional Programming",
                    "JVM",
                    "REPL",
                    "Web Development",
                },
            },
            new Skill
            {
                Id = "pgq8km6m164ucugxbk9fk",
                Name = "Cloudflare",
                Key = "cloudflare",
                Path = "/assets/skills/Cloudflare.svg",
                Keywords = new[]
                {
                    "Cloudflare",
                    "CDN",
                    "DDoS Protection",
                    "Security",
                    "DNS",
                    "Web Performance",
                    "Internet Services",
                    "Firewall",
                    "Networking",
                },
            },
            new Skill
            {
                Id = "m5kqdg9zmgspixa5vviif",
                Name = "Cloudflare Workers",
                Key = "cloudflare-workers",
                Path = "/assets/skills/Cloudflare-Workers.svg",
                Keywords = new[] { "Cloudflare Workers", "Serverless", "Edge Computing", "Cloudflare", "JavaScript", "Web Development", "APIs", "Scalable" },
            },
            new Skill
            {
                Id = "tc5b6ejqbiwkkjqkgsz8j",
                Name = "CMake",
                Key = "cmake",
                Path = "/assets/skills/CMake.svg",
                Keywords = new[] { "CMake", "Build System", "C++", "C", "Cross-Platform", "Open Source", "Build Automation", "Compilation" },
            },
            new Skill
            {
                Id = "8cysxosodcg6srke23yvs",
                Name = "Codeac",
                Key = "codeac",
                Path = "/assets/skills/Codeac.svg",
                Keywords = new[] { "Codeac", "Code Quality", "Static Analysis", "CI/CD", "DevOps", "Automation", "Code Review" },
            },
            new Skill
            {
                Id = "br8c26rneta7z8s2126bg",
                Name = "Codecov",
                Key = "codecov",
                Path = "/assets/skills/Codecov.svg",
                Keywords = new[] { "Codecov", "Code Coverage", "Testing", "CI/CD", "DevOps", "Automation", "Code Quality", "Version Control" },
            },
            new Skill
            {
                Id = "zqc45gzszy9zot5u7gr85",
                Name = "CodeIgniter",
                Key = "codeigniter",
                Path = "/assets/skills/CodeIgniter.svg",
                Keywords = new[] { "CodeIgniter", "PHP", "Web Development", "MVC Framework", "Backend Development", "Open Source", "Rapid Development" },
            },
            new Skill
            {
                Id = "k8wy1rd62p1wm99dgjm7b",
                Name = "CodePen",
                Key = "codepen",
                Path = "/assets/skills/CodePen.svg",
                Keywords = new[] { "CodePen", "Frontend Development", "Web Development", "HTML", "CSS", "JavaScript", "Code Sharing", "Prototyping" },
            },
            new Skill
            {
                Id = "cd6m5u3m8opvwbaem1tw4",
                Name = "CoffeeScript",
                Key = "coffeescript",
                Path = "/assets/skills/CoffeeScript.svg",
                Keywords = new[]
                {
                    "CoffeeScript",
                    "JavaScript",
                    "Programming Language",
                    "Frontend Development",
                    "Web Development",
                    "Scripting Language",
                    "Ruby",
                },
            },
            new Skill
            {
                Id = "6g4tk6uv4yb1k44ygaikw",
                Name = "Composer",
                Key = "composer",
                Path = "/assets/skills/Composer.svg",
                Keywords = new[] { "Composer", "PHP", "Dependency Management", "Package Manager", "Backend Development", "Open Source", "Autoloading" },
            },
            new Skill
            {
                Id = "dsije6nis4kxu86pds498",
                Name = "Confluence",
                Key = "confluence",
                Path = "/assets/skills/Confluence.svg",
                Keywords = new[]
                {
                    "Confluence",
                    "Collaboration",
                    "Documentation",
                    "Wiki",
                    "Atlassian",
                    "Project Management",
                    "Team Collaboration",
                    "Knowledge Management",
                },
            },
            new Skill
            {
                Id = "siqowiy9655hir1mhoc4h",
                Name = "Consul",
                Key = "consul",
                Path = "/assets/skills/Consul.svg",
                Keywords = new[]
                {
                    "Consul",
                    "Service Mesh",
                    "Service Discovery",
                    "Key-Value Store",
                    "Infrastructure",
                    "HashiCorp",
                    "Networking",
                    "Distributed Systems",
                    "DevOps",
                },
            },
            new Skill
            {
                Id = "1sxrd6dmosrkr2yc2y2m5",
                Name = "Contao",
                Key = "contao",
                Path = "/assets/skills/Contao.svg",
                Keywords = new[] { "Contao", "CMS", "Content Management System", "Web Development", "PHP", "Open Source", "Responsive Design" },
            },
            new Skill
            {
                Id = "k5hwpxc4tcyndz74eun11",
                Name = "Core-js",
                Key = "core-js",
                Path = "/assets/skills/Core-js.svg",
                Keywords = new[] { "Core-js", "JavaScript", "Polyfill", "ECMAScript", "Web Development", "Compatibility", "Frontend Development" },
            },
            new Skill
            {
                Id = "dssvm8dnqv397xwinw8zo",
                Name = "Cosmos DB",
                Key = "cosmos-db",
                Path = "/assets/skills/Cosmos-BD.svg",
                Keywords = new[]
                {
                    "Cosmos DB",
                    "NoSQL Database",
                    "Microsoft Azure",
                    "Cloud Database",
                    "Global Distribution",
                    "Scalability",
                    "Multi-Model Database",
                    "JSON",
                },
            },
            new Skill
            {
                Id = "1hzzaqgtmcofzkftydqes",
                Name = "CouchDB",
                Key = "couchdb",
                Path = "/assets/skills/CouchDB.svg",
                Keywords = new[] { "CouchDB", "NoSQL Database", "Document Store", "JSON", "Open Source", "RESTful API", "Replication", "Scalability" },
            },
            new Skill
            {
                Id = "okj5756jbgkd4m7ohtcbm",
                Name = "Crystal",
                Key = "crystal",
                Path = "/assets/skills/Crystal.svg",
                Keywords = new[] { "Crystal", "Programming Language", "Ruby-Like", "Compiled Language", "High Performance", "Concurrency", "Type Safety" },
            },
            new Skill
            {
                Id = "36mm89p9hks8baze1rdog",
                Name = "CSS3",
                Key = "css3",
                Path = "/assets/skills/CSS3.svg",
                Keywords = new[]
                {
                    "CSS3",
                    "CSS",
                    "Frontend Development",
                    "Web Design",
                    "Styling",
                    "Responsive Design",
                    "HTML",
                    "Animation",
                    "Web Development",
                },
            },
            new Skill
            {
                Id = "29va1qnthi8o7nj1usvj3",
                Name = "Cucumber",
                Key = "cucumber",
                Path = "/assets/skills/Cucumber.svg",
                Keywords = new[] { "Cucumber", "BDD", "Behavior-Driven Development", "Testing", "Automation", "Gherkin", "Test Automation", "Agile" },
            },
            new Skill
            {
                Id = "1c3jubcdthy74gd5o87ct",
                Name = "Cypress",
                Key = "cypress",
                Path = "/assets/skills/Cypress.svg",
                Keywords = new[] { "Cypress", "End-to-End Testing", "Frontend Development", "JavaScript", "Testing", "Automation", "CI/CD", "DevOps" },
            },
            new Skill
            {
                Id = "c7nbu3e9c3mov4jk6152r",
                Name = "D3.js",
                Key = "d3-js",
                Path = "/assets/skills/D3.js.svg",
                Keywords = new[] { "D3.js", "Data Visualization", "JavaScript", "Charts", "Graphs", "Frontend Development", "SVG", "Web Development" },
            },
            new Skill
            {
                Id = "ojk9rh1qudcct1nkyimjm",
                Name = "Dart",
                Key = "dart",
                Path = "/assets/skills/Dart.svg",
                Keywords = new[] { "Dart", "Programming Language", "Flutter", "Web Development", "Mobile Development", "Google", "Frontend Development" },
            },
            new Skill
            {
                Id = "ash4isyb31k2efg4kwuek",
                Name = "DataGrip",
                Key = "datagrip",
                Path = "/assets/skills/DataGrip.svg",
                Keywords = new[]
                {
                    "DataGrip",
                    "Database Management",
                    "SQL",
                    "JetBrains",
                    "Database Tool",
                    "Database Administration",
                    "Query Optimization",
                    "Data Modeling",
                },
            },
            new Skill
            {
                Id = "a5qn35sm24u8wj9zyfiw3",
                Name = "DataSpell",
                Key = "dataspell",
                Path = "/assets/skills/DataSpell.svg",
                Keywords = new[] { "DataSpell", "Data Science", "JetBrains", "IDE", "Python", "Machine Learning", "Jupyter Notebooks", "Data Analysis" },
            },
            new Skill
            {
                Id = "xanqip5a8cm5k78g1hvpf",
                Name = "DBeaver",
                Key = "dbeaver",
                Path = "/assets/skills/DBeaver.svg",
                Keywords = new[] { "DBeaver", "Database Management", "SQL", "Open Source", "Database Tool", "Database Administration", "JDBC" },
            },
            new Skill
            {
                Id = "q2c8kzham93uirvea7zjh",
                Name = "Debian",
                Key = "debian",
                Path = "/assets/skills/Debian.svg",
                Keywords = new[] { "Debian", "Linux", "Operating System", "Open Source", "Server", "Package Management", "Stable Release" },
            },
            new Skill
            {
                Id = "aanf17kjbkf5at8y5mh8j",
                Name = "Deno",
                Key = "deno",
                Path = "/assets/skills/Deno.svg",
                Keywords = new[] { "Deno", "JavaScript", "TypeScript", "Runtime", "Web Development", "Node.js Alternative", "Secure", "Server-Side" },
            },
            new Skill
            {
                Id = "jgjak2w2mqx11de9ieu3m",
                Name = "Devicon",
                Key = "devicon",
                Path = "/assets/skills/Devicon.svg",
                Keywords = new[] { "Devicon", "Icons", "Development", "Programming Languages", "Tools", "Frontend Development", "Design" },
            },
            new Skill
            {
                Id = "adoc5yqfs1afpwm823tat",
                Name = "Digital Ocean",
                Key = "digital-ocean",
                Path = "/assets/skills/Digital-Ocean.svg",
                Keywords = new[] { "Digital Ocean", "Cloud Computing", "VPS", "Hosting", "IaaS", "Infrastructure", "Containers", "Kubernetes" },
            },
            new Skill
            {
                Id = "4kb5vy1va8h9xopqz5s4b",
                Name = "Discord.js",
                Key = "discord-js",
                Path = "/assets/skills/Discord.js.svg",
                Keywords = new[] { "Discord.js", "JavaScript", "Bot Development", "Discord API", "Node.js", "Chatbot", "Realtime Communication" },
            },
            new Skill
            {
                Id = "re1bt8r5g6hqzfp68no59",
                Name = "Django",
                Key = "django",
                Path = "/assets/skills/Django.svg",
                Keywords = new[] { "Django", "Python", "Web Development", "Backend Development", "MVC Framework", "ORM", "Full Stack", "Authentication" },
            },
            new Skill
            {
                Id = "b83yjvyyj5iidiz35j6hc",
                Name = "Django REST",
                Key = "django-rest",
                Path = "/assets/skills/Django-REST.svg",
                Keywords = new[]
                {
                    "Django REST",
                    "Python",
                    "RESTful API",
                    "Backend Development",
                    "Web Development",
                    "Django",
                    "Serialization",
                    "Authentication",
                    "DRF",
                },
            },
            new Skill
            {
                Id = "nvvfz6skd23a5u38ebfgr",
                Name = "Docker",
                Key = "docker",
                Path = "/assets/skills/Docker.svg",
                Keywords = new[] { "Docker", "Containerization", "DevOps", "CI/CD", "Microservices", "Kubernetes", "Container Orchestration", "Cloud" },
            },
            new Skill
            {
                Id = "vf4fo1t3bqz6jw2u5d2yg",
                Name = "Doctrine",
                Key = "doctrine",
                Path = "/assets/skills/Doctrine.svg",
                Keywords = new[] { "Doctrine", "PHP", "ORM", "Database Abstraction", "Backend Development", "Web Development", "Data Mapping" },
            },
            new Skill
            {
                Id = "vtr4kx52qp4xsiqwkryss",
                Name = "Dropwizard",
                Key = "dropwizard",
                Path = "/assets/skills/Dropwizard.svg",
                Keywords = new[] { "Dropwizard", "Java", "Microservices", "RESTful API", "Web Development", "Backend Development", "Jersey", "Jackson" },
            },
            new Skill
            {
                Id = "ntnekaxzr244tdiebhdby",
                Name = "Drupal",
                Key = "drupal",
                Path = "/assets/skills/Drupal.svg",
                Keywords = new[] { "Drupal", "CMS", "Content Management System", "Web Development", "PHP", "Open Source", "Frontend Development" },
            },
            new Skill
            {
                Id = "xvoqrryu5cai6r1g7y61x",
                Name = "Eclipse Ceylon",
                Key = "eclipse-ceylon",
                Path = "/assets/skills/Eclipse-Ceylon.svg",
                Keywords = new[] { "Eclipse Ceylon", "Programming Language", "JVM", "Modular Programming", "Type Safety", "Java" },
            },
            new Skill
            {
                Id = "7ecnnze2qtx8nx7v348dv",
                Name = "Eclipse IDE",
                Key = "eclipse-ide",
                Path = "/assets/skills/Eclipse-IDE.svg",
                Keywords = new[] { "Eclipse IDE", "Java", "IDE", "Development Environment", "Open Source", "Cross-Platform", "Refactoring", "Code Analysis" },
            },
            new Skill
            {
                Id = "ajfkvmrwcdkzh8kmbyatu",
                Name = "Eclipse Vert.x",
                Key = "eclipse-vertx",
                Path = "/assets/skills/Eclipse-Vert.x.svg",
                Keywords = new[]
                {
                    "Eclipse Vert.x",
                    "Reactive Programming",
                    "Java",
                    "Event-Driven",
                    "Microservices",
                    "Scalability",
                    "Asynchronous Programming",
                },
            },
            new Skill
            {
                Id = "h71sdgd9qnuh3q8s1b3o7",
                Name = "Elastic Beats",
                Key = "elastic-beats",
                Path = "/assets/skills/Elastic-Beats.svg",
                Keywords = new[] { "Elastic Beats", "Log Shipping", "Elastic Stack", "Monitoring", "Data Collection" },
            },
            new Skill
            {
                Id = "5cgenbd91975wrijvv6ts",
                Name = "Elastic Search",
                Key = "elastic-search",
                Path = "/assets/skills/Elastic-Search.svg",
                Keywords = new[] { "Elastic Search", "Search Engine", "Full-Text Search", "Elasticsearch", "Data Indexing" },
            },
            new Skill
            {
                Id = "6y2f9kya6gq5k62ggrqro",
                Name = "Electron",
                Key = "electron",
                Path = "/assets/skills/Electron.svg",
                Keywords = new[] { "Electron", "JavaScript", "Desktop Applications", "Cross-Platform", "Web Technologies", "Node.js", "Chromium" },
            },
            new Skill
            {
                Id = "j8m5yownz3zjyfdf9yac9",
                Name = "Eleventy (11ty)",
                Key = "eleventy",
                Path = "/assets/skills/Eleventy-(11ty).svg",
                Keywords = new[] { "Eleventy", "Static Site Generator", "JavaScript", "Web Development", "JAMstack" },
            },
            new Skill
            {
                Id = "dyydpj6wn9qdh6jvie8rt",
                Name = "Elixir",
                Key = "elixir",
                Path = "/assets/skills/Elixir.svg",
                Keywords = new[]
                {
                    "Elixir",
                    "Functional Programming",
                    "Concurrency",
                    "Erlang",
                    "Scalability",
                    "Distributed Systems",
                    "Phoenix Framework",
                    "BEAM",
                },
            },
            new Skill
            {
                Id = "kd9a3jw7kiiod3z7ckgv1",
                Name = "Elm",
                Key = "elm",
                Path = "/assets/skills/Elm.svg",
                Keywords = new[] { "Elm", "Frontend Development", "Functional Programming", "JavaScript", "Web Development", "Strong Typing", "Immutability" },
            },
            new Skill
            {
                Id = "xxfekn52k5qhkx64mdnj5",
                Name = "Embedded C",
                Key = "embedded-c",
                Path = "/assets/skills/Embedded-C.svg",
                Keywords = new[] { "Embedded C", "C Programming", "Embedded Systems", "Low-Level Programming", "Microcontrollers" },
            },
            new Skill
            {
                Id = "9b8tgbqg84fd76tsm1if6",
                Name = "Ember.js",
                Key = "ember-js",
                Path = "/assets/skills/Ember.js.svg",
                Keywords = new[] { "Ember.js", "JavaScript", "Frontend Development", "Single-Page Applications", "SPAs", "Web Development", "Templating" },
            },
            new Skill
            {
                Id = "fqkknr4r9be51d9t5crz6",
                Name = "Envoy",
                Key = "envoy",
                Path = "/assets/skills/Envoy.svg",
                Keywords = new[] { "Envoy", "Proxy", "Service Mesh", "Microservices", "Networking" },
            },
            new Skill
            {
                Id = "3s2n6un6vua59r77yhchp",
                Name = "Erlang",
                Key = "erlang",
                Path = "/assets/skills/Erlang.svg",
                Keywords = new[]
                {
                    "Erlang",
                    "Functional Programming",
                    "Concurrency",
                    "Telecommunications",
                    "Distributed Systems",
                    "Fault-Tolerance",
                    "Scalability",
                    "OTP",
                },
            },
            new Skill
            {
                Id = "g2i4kxcdkf4wmdp5134pf",
                Name = "ESLint",
                Key = "eslint",
                Path = "/assets/skills/ESLint.svg",
                Keywords = new[]
                {
                    "ESLint",
                    "JavaScript",
                    "Code Quality",
                    "Linting",
                    "Static Analysis",
                    "Frontend Development",
                    "Code Formatting",
                    "Best Practices",
                },
            },
            new Skill
            {
                Id = "mfkhcmrn2g8jgshk12j6u",
                Name = "Express",
                Key = "express",
                Path = "/assets/skills/Express.svg",
                Keywords = new[] { "Express", "Node.js", "JavaScript", "Backend Development", "Web Development", "RESTful API", "Middleware", "Routing" },
            },
            new Skill
            {
                Id = "aosh4jzpoxycdcy6o6koj",
                Name = "F#",
                Key = "fsharp",
                Path = "/assets/skills/fsharp.svg",
                Keywords = new[] { "F#", "Functional Programming", "Microsoft", "Type Safety", "Concurrency", ".NET", "Backend Development" },
            },
            new Skill
            {
                Id = "oodognhhxab95mohkk3q1",
                Name = "Facebook",
                Key = "facebook",
                Path = "/assets/skills/Facebook.svg",
                Keywords = new[] { "Facebook", "Social Media", "Marketing", "Communication", "Networking" },
            },
            new Skill
            {
                Id = "d2wfnco1cqz1udhrqqmcv",
                Name = "FastAPI",
                Key = "fastapi",
                Path = "/assets/skills/FastAPI.svg",
                Keywords = new[] { "FastAPI", "Python", "Web Development", "RESTful API", "Backend Development", "Asynchronous Programming", "Microservices" },
            },
            new Skill
            {
                Id = "1uuu8yweq197b1vc2enes",
                Name = "Fastify",
                Key = "fastify",
                Path = "/assets/skills/Fastify.svg",
                Keywords = new[] { "Fastify", "Node.js", "JavaScript", "Web Development", "Backend" },
            },
            new Skill
            {
                Id = "x8rohktqz3jozs42bc91e",
                Name = "Fauna",
                Key = "fauna",
                Path = "/assets/skills/Fauna.svg",
                Keywords = new[] { "Fauna", "Database", "Serverless", "GraphQL", "Cloud" },
            },
            new Skill
            {
                Id = "bm1zngvruap41ok71jeam",
                Name = "Feathers",
                Key = "feathers",
                Path = "/assets/skills/Feathers.svg",
                Keywords = new[] { "Feathers", "JavaScript", "Node.js", "Microservices", "RESTful API" },
            },
            new Skill
            {
                Id = "c9b2tjzppokbc8kpp9ox2",
                Name = "Fedora",
                Key = "fedora",
                Path = "/assets/skills/Fedora.svg",
                Keywords = new[] { "Fedora", "Linux", "Operating System", "Open Source", "Workstation" },
            },
            new Skill
            {
                Id = "8tcimd8dtbqoqrgmbme6k",
                Name = "Figma",
                Key = "figma",
                Path = "/assets/skills/Figma.svg",
                Keywords = new[]
                {
                    "Figma",
                    "UI/UX Design",
                    "Prototyping",
                    "Collaboration",
                    "Web Design",
                    "Vector Graphics",
                    "User Interface",
                    "User Experience",
                },
            },
            new Skill
            {
                Id = "56yy5d6b8b17jrbbeaff1",
                Name = "FileZilla",
                Key = "filezilla",
                Path = "/assets/skills/FileZilla.svg",
                Keywords = new[] { "FileZilla", "FTP Client", "File Transfer", "Open Source", "Networking" },
            },
            new Skill
            {
                Id = "5q6iycevfv2iw7g3uhvzv",
                Name = "Firebase",
                Key = "firebase",
                Path = "/assets/skills/Firebase.svg",
                Keywords = new[]
                {
                    "Firebase",
                    "Backend as a Service",
                    "BaaS",
                    "Google",
                    "Realtime Database",
                    "Authentication",
                    "Cloud Firestore",
                    "Serverless",
                },
            },
            new Skill
            {
                Id = "5oywj5fxvrqwvhx8ixj9p",
                Name = "Firefox",
                Key = "firefox",
                Path = "/assets/skills/Firefox.svg",
                Keywords = new[] { "Firefox", "Web Browser", "Mozilla", "Internet", "Open Source" },
            },
            new Skill
            {
                Id = "vxupvy3zjivasra64uo1n",
                Name = "Flask",
                Key = "flask",
                Path = "/assets/skills/Flask.svg",
                Keywords = new[] { "Flask", "Python", "Web Development", "Backend Development", "Microservices", "RESTful API", "MVC Framework", "Jinja2" },
            },
            new Skill
            {
                Id = "j2sfwkrsvr6h4b85co5wh",
                Name = "Flutter",
                Key = "flutter",
                Path = "/assets/skills/Flutter.svg",
                Keywords = new[]
                {
                    "Flutter",
                    "Mobile Development",
                    "Cross-Platform",
                    "Google",
                    "Dart",
                    "App Development",
                    "UI Framework",
                    "Frontend Development",
                },
            },
            new Skill
            {
                Id = "e8doth9t7hv219hy27jvv",
                Name = "Fortran",
                Key = "fortran",
                Path = "/assets/skills/Fortran.svg",
                Keywords = new[] { "Fortran", "Programming Language", "Scientific Computing", "Mathematics", "Numerical Analysis" },
            },
            new Skill
            {
                Id = "7d8qrhibuchvhhstdn3x4",
                Name = "Foundation",
                Key = "foundation",
                Path = "/assets/skills/Foundation.svg",
                Keywords = new[]
                {
                    "Foundation",
                    "CSS Framework",
                    "Frontend Development",
                    "Responsive Design",
                    "Web Development",
                    "UI Components",
                    "HTML",
                    "JavaScript",
                },
            },
            new Skill
            {
                Id = "5y6vw3me5wbsd97bkcg1w",
                Name = "Gatling",
                Key = "gatling",
                Path = "/assets/skills/Gatling.svg",
                Keywords = new[] { "Gatling", "Load Testing", "Performance Testing", "Web Applications", "Open Source" },
            },
            new Skill
            {
                Id = "o7qcwcpcokexu7g2zwy4o",
                Name = "Gatsby",
                Key = "gatsby",
                Path = "/assets/skills/Gatsby.svg",
                Keywords = new[] { "Gatsby", "React", "Static Site Generator", "JavaScript", "Web Development", "GraphQL", "Frontend Development", "JAMstack" },
            },
            new Skill
            {
                Id = "zz9kwwubanbqxjdzmj592",
                Name = "Gazebo",
                Key = "gazebo",
                Path = "/assets/skills/Gazebo.svg",
                Keywords = new[] { "Gazebo", "Robotics", "Simulation", "3D Modeling", "Physics Engine" },
            },
            new Skill
            {
                Id = "ixgjib6refcd157xuvoxf",
                Name = "GCC",
                Key = "gcc",
                Path = "/assets/skills/GCC.svg",
                Keywords = new[] { "GCC", "Compiler", "C/C++", "GNU", "Open Source" },
            },
            new Skill
            {
                Id = "d6dhan2pkojb7okrazw6i",
                Name = "Gentoo",
                Key = "gentoo",
                Path = "/assets/skills/Gentoo.svg",
                Keywords = new[] { "Gentoo", "Linux", "Operating System", "Open Source", "Portage" },
            },
            new Skill
            {
                Id = "65qgac5143j567hy86vyy",
                Name = "Ghost",
                Key = "ghost",
                Path = "/assets/skills/Ghost.svg",
                Keywords = new[] { "Ghost", "Blogging", "CMS", "Node.js", "Web Development" },
            },
            new Skill
            {
                Id = "h1z4voqrm3trrrh8djgjk",
                Name = "GIMP",
                Key = "gimp",
                Path = "/assets/skills/GIMP.svg",
                Keywords = new[] { "GIMP", "Graphic Design", "Photo Editing", "Open Source", "Image Manipulation", "Digital Art" },
            },
            new Skill
            {
                Id = "brdcidsp4o1p3fy9o78ar",
                Name = "Git",
                Key = "git",
                Path = "/assets/skills/Git.svg",
                Keywords = new[] { "Git", "Version Control", "Source Code Management", "SCM", "Branching", "Merging", "Commit", "Open Source" },
            },
            new Skill
            {
                Id = "xq3nhem6idvjc1ufrey5r",
                Name = "GitBook",
                Key = "gitbook",
                Path = "/assets/skills/GitBook.svg",
                Keywords = new[] { "GitBook", "Documentation", "Markdown", "Collaboration", "Open Source" },
            },
            new Skill
            {
                Id = "444jzfizaq6pkvh6ixbby",
                Name = "GitHub",
                Key = "github",
                Path = "/assets/skills/GitHub.svg",
                Keywords = new[]
                {
                    "GitHub",
                    "Version Control",
                    "Source Code Management",
                    "SCM",
                    "Open Source",
                    "Collaboration",
                    "Repository",
                    "Pull Requests",
                },
            },
            new Skill
            {
                Id = "22yzvs5fuq74qgfanooqw",
                Name = "GitHub Actions",
                Key = "github-actions",
                Path = "/assets/skills/GitHub-Actions.svg",
                Keywords = new[] { "GitHub Actions", "CI/CD", "Continuous Integration", "Continuous Delivery", "Automation", "DevOps", "Workflows", "GitHub" },
            },
            new Skill
            {
                Id = "h3gaevqqxp43s8cvug2fr",
                Name = "GitHub Codespaces",
                Key = "github-codespaces",
                Path = "/assets/skills/GitHub-Codespaces.svg",
                Keywords = new[] { "GitHub Codespaces", "Cloud Development", "IDE", "GitHub", "Remote Development" },
            },
            new Skill
            {
                Id = "2aqwutocp12qdnxgjjw5g",
                Name = "GitLab",
                Key = "gitlab",
                Path = "/assets/skills/GitLab.svg",
                Keywords = new[] { "GitLab", "Version Control", "Source Code Management", "SCM", "CI/CD", "DevOps", "Automation", "Git" },
            },
            new Skill
            {
                Id = "bb9f882cz7izttf4gwxgq",
                Name = "Gitpod",
                Key = "gitpod",
                Path = "/assets/skills/Gitpod.svg",
                Keywords = new[] { "Gitpod", "Cloud Development", "IDE", "Remote Development", "Open Source" },
            },
            new Skill
            {
                Id = "feayf6hptp2hqkox7e4sv",
                Name = "Gitter",
                Key = "gitter",
                Path = "/assets/skills/Gitter.svg",
                Keywords = new[] { "Gitter", "Collaboration", "Chat", "Open Source", "Communication", "Developer Community" },
            },
            new Skill
            {
                Id = "znjr3pru6d7jtgpzdcx7s",
                Name = "Gleam",
                Key = "gleam",
                Path = "/assets/skills/Gleam.svg",
                Keywords = new[] { "Gleam", "Programming Language", "Functional Programming", "Erlang", "BEAM", "Concurrency" },
            },
            new Skill
            {
                Id = "gjfsyxsnkpmu3uob4jz54",
                Name = "GNU Emacs",
                Key = "gnu-emacs",
                Path = "/assets/skills/GNU-Emacs.svg",
                Keywords = new[] { "GNU Emacs", "Text Editor", "Open Source", "Customization", "Scripting" },
            },
            new Skill
            {
                Id = "1xxgce1wtjshn9w3bpwhe",
                Name = "GnuPG",
                Key = "gnupg",
                Path = "/assets/skills/GnuPG.svg",
                Keywords = new[] { "GnuPG", "Encryption", "Open Source", "Security", "Cryptography", "GPG", "Digital Signatures" },
            },
            new Skill
            {
                Id = "m3cwgy5562uwm3yjr3nio",
                Name = "Go",
                Key = "go",
                Path = "/assets/skills/Go.svg",
                Keywords = new[] { "Go", "Golang", "Programming Language", "Concurrency", "Microservices", "Backend Development", "Google", "Scalability" },
            },
            new Skill
            {
                Id = "ty3xzwj52wpr3yw4qdduo",
                Name = "Godot",
                Key = "godot",
                Path = "/assets/skills/Godot.svg",
                Keywords = new[] { "Godot", "Game Development", "Open Source", "2D Games", "3D Games", "GDScript", "C#", "Cross-Platform" },
            },
            new Skill
            {
                Id = "1rn7141re11fruaiwg8tu",
                Name = "Godot Engine",
                Key = "godot-engine",
                Path = "/assets/skills/Godot-Engine.svg",
                Keywords = new[] { "Godot Engine", "Game Development", "Open Source", "2D/3D Games", "GDScript" },
            },
            new Skill
            {
                Id = "wu2mssdweqv789nefto5t",
                Name = "GoLand",
                Key = "goland",
                Path = "/assets/skills/GoLand.svg",
                Keywords = new[] { "GoLand", "JetBrains", "IDE", "Go", "Golang" },
            },
            new Skill
            {
                Id = "w3rnywypjr837nye5hce1",
                Name = "Google",
                Key = "google",
                Path = "/assets/skills/Google.svg",
                Keywords = new[] { "Google", "Search Engine", "Cloud Computing", "Technology", "Internet" },
            },
            new Skill
            {
                Id = "gg2z9b8hkdvprw6zuzih5",
                Name = "Google Cloud",
                Key = "google-cloud",
                Path = "/assets/skills/Google-Cloud.svg",
                Keywords = new[] { "Google Cloud", "Cloud Computing", "GCP", "Google", "Infrastructure", "Big Data", "Machine Learning", "Serverless" },
            },
            new Skill
            {
                Id = "ci7qrrf87bh8v421cerm3",
                Name = "Gradle",
                Key = "gradle",
                Path = "/assets/skills/Gradle.svg",
                Keywords = new[] { "Gradle", "Build Automation", "Java", "Groovy", "Kotlin", "Dependency Management", "CI/CD", "DevOps" },
            },
            new Skill
            {
                Id = "enua1j8m8f989kq1njwo6",
                Name = "Grafana",
                Key = "grafana",
                Path = "/assets/skills/Grafana.svg",
                Keywords = new[] { "Grafana", "Monitoring", "Visualization", "Metrics", "Dashboards" },
            },
            new Skill
            {
                Id = "zm3v83xzcuirm1zx2r14j",
                Name = "Grails",
                Key = "grails",
                Path = "/assets/skills/Grails.svg",
                Keywords = new[] { "Grails", "Groovy", "Java", "Web Development", "MVC Framework" },
            },
            new Skill
            {
                Id = "z4fdo2tdgfajaefaa6jnd",
                Name = "GraphQL",
                Key = "graphql",
                Path = "/assets/skills/GraphQL.svg",
                Keywords = new[] { "GraphQL", "API", "Web Development", "Frontend Development", "Query Language", "Data Fetching", "Facebook" },
            },
            new Skill
            {
                Id = "69xzzopm49ig22d6gd38j",
                Name = "Grunt",
                Key = "grunt",
                Path = "/assets/skills/Grunt.svg",
                Keywords = new[] { "Grunt", "JavaScript", "Task Runner", "Build Tools", "Automation", "Web Development", "Frontend Development" },
            },
            new Skill
            {
                Id = "fs7o9cymkhkmb7xwchqjr",
                Name = "Grunt.js",
                Key = "grunt-js",
                Path = "/assets/skills/Grunt.js.svg",
                Keywords = new[] { "Grunt.js", "JavaScript", "Task Runner", "Automation", "Web Development" },
            },
            new Skill
            {
                Id = "s58z9jzmcinejny25xbbn",
                Name = "Gulp",
                Key = "gulp",
                Path = "/assets/skills/Gulp.svg",
                Keywords = new[] { "Gulp", "JavaScript", "Task Runner", "Build Tools", "Automation", "Web Development", "Frontend Development" },
            },
            new Skill
            {
                Id = "n5vt2i24mtkiyu6oajf6n",
                Name = "Handlebars",
                Key = "handlebars",
                Path = "/assets/skills/Handlebars.svg",
                Keywords = new[] { "Handlebars", "JavaScript", "Templating", "Web Development", "Frontend" },
            },
            new Skill
            {
                Id = "wojj7vi61mkjsex23pm38",
                Name = "Hardhat",
                Key = "hardhat",
                Path = "/assets/skills/Hardhat.svg",
                Keywords = new[] { "Hardhat", "Blockchain", "Smart Contracts", "Ethereum", "Web3" },
            },
            new Skill
            {
                Id = "ge4zgte49xq3favnrvnu3",
                Name = "Harvester",
                Key = "harvester",
                Path = "/assets/skills/Harvester.svg",
                Keywords = new[] { "Harvester", "Kubernetes", "Hyper-Converged", "Cloud", "Virtualization" },
            },
            new Skill
            {
                Id = "j74o985qgsohsp28nxmqa",
                Name = "HashiCorp Terraform",
                Key = "hashicorp-terraform",
                Path = "/assets/skills/HashiCorp-Terraform.svg",
                Keywords = new[] { "HashiCorp Terraform", "Infrastructure as Code", "IaC", "Automation", "DevOps" },
            },
            new Skill
            {
                Id = "9ds9ivpifoqnpxyae3hb9",
                Name = "HashiCorp Vagrant",
                Key = "hashicorp-vagrant",
                Path = "/assets/skills/HashiCorp-Vagrant.svg",
                Keywords = new[] { "HashiCorp Vagrant", "Virtualization", "Development Environments", "Automation", "DevOps" },
            },
            new Skill
            {
                Id = "fpmurzhywomcjapg5c9vt",
                Name = "HashiCorp Vault",
                Key = "hashicorp-vault",
                Path = "/assets/skills/HashiCorp-Vault.svg",
                Keywords = new[] { "HashiCorp Vault", "Secrets Management", "Security", "Encryption", "DevOps" },
            },
            new Skill
            {
                Id = "9gtjz4562g2761t9m6ue2",
                Name = "Haskell",
                Key = "haskell",
                Path = "/assets/skills/Haskell.svg",
                Keywords = new[] { "Haskell", "Functional Programming", "Type Safety", "Concurrency", "Pure Functions", "Strong Typing", "Laziness" },
            },
            new Skill
            {
                Id = "4fyr6tc7xp1eumq24xxyv",
                Name = "Haxe",
                Key = "haxe",
                Path = "/assets/skills/Haxe.svg",
                Keywords = new[] { "Haxe", "Programming Language", "Cross-Platform", "Game Development", "Web Development" },
            },
            new Skill
            {
                Id = "xvkewjdtugoa9drnz7jss",
                Name = "Helm",
                Key = "helm",
                Path = "/assets/skills/Helm.svg",
                Keywords = new[] { "Helm", "Kubernetes", "Package Management", "Cloud", "Automation" },
            },
            new Skill
            {
                Id = "nc6bg5kufujcxozh6a7gj",
                Name = "Heroku",
                Key = "heroku",
                Path = "/assets/skills/Heroku.svg",
                Keywords = new[] { "Heroku", "PaaS", "Cloud Computing", "Deployment", "Web Applications" },
            },
            new Skill
            {
                Id = "i6dvrg2zfnj6qdvg2925a",
                Name = "Hibernate",
                Key = "hibernate",
                Path = "/assets/skills/Hibernate.svg",
                Keywords = new[] { "Hibernate", "Java", "ORM", "Database Abstraction", "Backend Development", "Web Development", "Data Mapping" },
            },
            new Skill
            {
                Id = "w3n9r3tkwij3ez1ejs73y",
                Name = "Homebrew",
                Key = "homebrew",
                Path = "/assets/skills/Homebrew.svg",
                Keywords = new[] { "Homebrew", "Package Manager", "macOS", "Linux", "Open Source", "Software Installation" },
            },
            new Skill
            {
                Id = "c4seqp7v8ndw2yp177qow",
                Name = "HTML5",
                Key = "html5",
                Path = "/assets/skills/HTML5.svg",
                Keywords = new[] { "HTML5", "HTML", "Web Development", "Frontend Development", "Markup Language", "Responsive Design", "Cross-Browser" },
            },
            new Skill
            {
                Id = "ov51n4smuosdezxstxevg",
                Name = "Hugo",
                Key = "hugo",
                Path = "/assets/skills/Hugo.svg",
                Keywords = new[] { "Hugo", "Static Site Generator", "Go", "Web Development", "JAMstack", "Content Management" },
            },
            new Skill
            {
                Id = "adrtc5ft5mcaq584r41oz",
                Name = "IBM SPSS Statistics",
                Key = "ibm-spss-statistics",
                Path = "/assets/skills/IBM-SPSS-Statistics.svg",
                Keywords = new[] { "IBM SPSS Statistics", "Data Analysis", "Statistics", "Predictive Analytics", "Research" },
            },
            new Skill
            {
                Id = "5kbqqzw2sqcddqcxkis7t",
                Name = "IFTTT",
                Key = "ifttt",
                Path = "/assets/skills/IFTTT.svg",
                Keywords = new[] { "IFTTT", "Automation", "Integration", "APIs", "Workflow" },
            },
            new Skill
            {
                Id = "3mxcm1z6xvb39etdum3id",
                Name = "InfluxDB",
                Key = "influxdb",
                Path = "/assets/skills/InfluxDB.svg",
                Keywords = new[] { "InfluxDB", "Time Series Database", "Monitoring", "Data Storage", "Scalability" },
            },
            new Skill
            {
                Id = "5oo1dtpc8om1v8so6b2rf",
                Name = "Inkscape",
                Key = "inkscape",
                Path = "/assets/skills/Inkscape-.svg",
                Keywords = new[] { "Inkscape", "Vector Graphics", "Graphic Design", "Open Source", "Illustration" },
            },
            new Skill
            {
                Id = "4n8gp5aki9u8na15z2pxu",
                Name = "Insomnia",
                Key = "insomnia",
                Path = "/assets/skills/Insomnia.svg",
                Keywords = new[] { "Insomnia", "API Testing", "REST Client", "GraphQL", "HTTP", "Postman Alternative", "Web Development" },
            },
            new Skill
            {
                Id = "hsjqss34wqx7wgnkxyta8",
                Name = "IntelliJ IDEA",
                Key = "intellij-idea",
                Path = "/assets/skills/IntelliJ-IDEA.svg",
                Keywords = new[] { "IntelliJ IDEA", "Java", "IDE", "JetBrains", "Development Environment" },
            },
            new Skill
            {
                Id = "ea4ayvn2ppcmcqffme9q9",
                Name = "Internet Explorer 10 (IE10)",
                Key = "internet-explorer-10",
                Path = "/assets/skills/Internet-Explorer-10-(ie10).svg",
                Keywords = new[] { "Internet Explorer 10", "Web Browser", "Microsoft", "Internet", "Legacy Support" },
            },
            new Skill
            {
                Id = "vfz3nhzbnymmmb52ybau4",
                Name = "Ionic",
                Key = "ionic",
                Path = "/assets/skills/Ionic.svg",
                Keywords = new[] { "Ionic", "Mobile Development", "Cross-Platform", "Hybrid Apps", "Web Development", "Angular", "PWA", "UI Components" },
            },
            new Skill
            {
                Id = "eqtr77zraarqjmct36f3h",
                Name = "Jaeger Tracing",
                Key = "jaeger-tracing",
                Path = "/assets/skills/Jaeger-Tracing.svg",
                Keywords = new[] { "Jaeger Tracing", "Distributed Tracing", "Monitoring", "Microservices", "Performance" },
            },
            new Skill
            {
                Id = "ghryq6mttd4hys9t3934v",
                Name = "Jamstack",
                Key = "jamstack",
                Path = "/assets/skills/Jamstack.svg",
                Keywords = new[] { "Jamstack", "Web Development", "Static Sites", "JavaScript", "APIs" },
            },
            new Skill
            {
                Id = "chwh4njc1g52sup68tc2v",
                Name = "Jasmine",
                Key = "jasmine",
                Path = "/assets/skills/Jasmine.svg",
                Keywords = new[] { "Jasmine", "JavaScript", "Testing", "BDD", "Frontend" },
            },
            new Skill
            {
                Id = "jkdz4dcvxxqg4uwbxm2z4",
                Name = "Java",
                Key = "java",
                Path = "/assets/skills/Java.svg",
                Keywords = new[]
                {
                    "Java",
                    "Programming Language",
                    "JVM",
                    "Backend Development",
                    "Web Development",
                    "Android Development",
                    "Object-Oriented Programming",
                    "Spring",
                },
            },
            new Skill
            {
                Id = "rzuwkb7rqggy2e9t3e2ve",
                Name = "JavaScript",
                Key = "javascript",
                Path = "/assets/skills/JavaScript.svg",
                Keywords = new[]
                {
                    "JavaScript",
                    "Programming Language",
                    "Frontend Development",
                    "Web Development",
                    "Node.js",
                    "ES6",
                    "React",
                    "Vue",
                    "Angular",
                },
            },
            new Skill
            {
                Id = "h4po5crzshrw1otpr1zrd",
                Name = "Jeet",
                Key = "jeet",
                Path = "/assets/skills/Jeet.svg",
                Keywords = new[] { "Jeet", "CSS Framework", "Grid System", "Web Development", "Frontend" },
            },
            new Skill
            {
                Id = "66koriimhv6819svhteif",
                Name = "Jekyll",
                Key = "jekyll",
                Path = "/assets/skills/Jekyll.svg",
                Keywords = new[] { "Jekyll", "Static Site Generator", "Ruby", "Web Development", "Blogging" },
            },
            new Skill
            {
                Id = "vi7t2z99w8xah2rsqp7ca",
                Name = "Jenkins",
                Key = "jenkins",
                Path = "/assets/skills/Jenkins.svg",
                Keywords = new[]
                {
                    "Jenkins",
                    "CI/CD",
                    "Continuous Integration",
                    "Continuous Delivery",
                    "DevOps",
                    "Automation",
                    "Build Pipelines",
                    "Open Source",
                },
            },
            new Skill
            {
                Id = "boq1p2xx7hadxs9ns9do6",
                Name = "Jest",
                Key = "jest",
                Path = "/assets/skills/Jest.svg",
                Keywords = new[] { "Jest", "JavaScript", "Testing", "Unit Testing", "Frontend Development", "Test Runner", "Snapshots", "Mocking" },
            },
            new Skill
            {
                Id = "b9cdkkjy9nzmpqb4krwt3",
                Name = "JetBrains",
                Key = "jetbrains",
                Path = "/assets/skills/JetBrains.svg",
                Keywords = new[] { "JetBrains", "IDE", "Development Tools", "Programming", "Software Development" },
            },
            new Skill
            {
                Id = "c62x359kiq8m3ohobb156",
                Name = "Jira",
                Key = "jira",
                Path = "/assets/skills/Jira.svg",
                Keywords = new[] { "Jira", "Project Management", "Atlassian", "Agile", "Scrum", "Kanban", "Issue Tracking", "Collaboration" },
            },
            new Skill
            {
                Id = "debds8yrvvs3faqu4azqc",
                Name = "Jira Align",
                Key = "jira-align",
                Path = "/assets/skills/Jira-Align.svg",
                Keywords = new[] { "Jira Align", "Project Management", "Agile", "Atlassian", "Scrum" },
            },
            new Skill
            {
                Id = "ftujnz8ia14tondxtd8q5",
                Name = "JQuery",
                Key = "jquery",
                Path = "/assets/skills/jQuery.svg",
                Keywords = new[] { "JQuery", "JavaScript", "Frontend Development", "Web Development", "DOM Manipulation", "AJAX", "Events" },
            },
            new Skill
            {
                Id = "8vea9akqncmrdein5dowc",
                Name = "JSON",
                Key = "json",
                Path = "/assets/skills/JSON.svg",
                Keywords = new[] { "JSON", "JavaScript Object Notation", "Data Format", "APIs", "Web Development" },
            },
            new Skill
            {
                Id = "zujfiq9qarhnxesamphdb",
                Name = "Jule",
                Key = "jule",
                Path = "/assets/skills/Jule.svg",
                Keywords = new[] { "Jule", "Programming Language", "Compilers", "Low-Level", "Systems Programming" },
            },
            new Skill
            {
                Id = "hdc1r6zxfmo4tdskfbb2w",
                Name = "Julia",
                Key = "julia",
                Path = "/assets/skills/Julia.svg",
                Keywords = new[] { "Julia", "Programming Language", "Data Science", "Numerical Computing", "High Performance", "Parallel Computing" },
            },
            new Skill
            {
                Id = "vzxq4f8piwe9gozzi167f",
                Name = "JUnit",
                Key = "junit",
                Path = "/assets/skills/JUnit.svg",
                Keywords = new[] { "JUnit", "Java", "Testing", "Unit Testing", "TDD" },
            },
            new Skill
            {
                Id = "ngw8g4pzfi6kw69w1tcmi",
                Name = "Jupyter",
                Key = "jupyter",
                Path = "/assets/skills/Jupyter.svg",
                Keywords = new[] { "Jupyter", "Data Science", "Python", "Notebooks", "Machine Learning" },
            },
            new Skill
            {
                Id = "es1ywf5ng5hs4fnayg4h1",
                Name = "K3OS",
                Key = "k3os",
                Path = "/assets/skills/K3OS.svg",
                Keywords = new[] { "K3OS", "Kubernetes", "Operating System", "Lightweight", "Cloud-Native" },
            },
            new Skill
            {
                Id = "uy5kv2f54abgrqsfzupkj",
                Name = "K3s",
                Key = "k3s",
                Path = "/assets/skills/K3s.svg",
                Keywords = new[] { "K3s", "Kubernetes", "Lightweight", "Cloud-Native", "Container Orchestration" },
            },
            new Skill
            {
                Id = "1mvoefx5ju7hvccbmvkm6",
                Name = "Kaggle",
                Key = "kaggle",
                Path = "/assets/skills/Kaggle.svg",
                Keywords = new[] { "Kaggle", "Data Science", "Machine Learning", "Competitions", "Python" },
            },
            new Skill
            {
                Id = "2iuz38u16bmtioikqa27q",
                Name = "Karate Labs",
                Key = "karate-labs",
                Path = "/assets/skills/Karate-Labs.svg",
                Keywords = new[] { "Karate Labs", "API Testing", "Automation", "Java", "Performance Testing" },
            },
            new Skill
            {
                Id = "efhis1ijhim6dqmygxiuw",
                Name = "Karma",
                Key = "karma",
                Path = "/assets/skills/Karma.svg",
                Keywords = new[] { "Karma", "JavaScript", "Testing", "Test Runner", "Continuous Integration" },
            },
            new Skill
            {
                Id = "bdzmowcd7fwjjoakeswz1",
                Name = "Keras",
                Key = "keras",
                Path = "/assets/skills/Keras.svg",
                Keywords = new[] { "Keras", "Machine Learning", "Deep Learning", "Python", "Neural Networks" },
            },
            new Skill
            {
                Id = "p49gebeiny79rfhyhyc68",
                Name = "Kibana",
                Key = "kibana",
                Path = "/assets/skills/Kibana.svg",
                Keywords = new[] { "Kibana", "Elastic Stack", "Visualization", "Monitoring", "Log Analysis" },
            },
            new Skill
            {
                Id = "eg7ddae7qobazhspkkzux",
                Name = "Knex.js",
                Key = "knex-js",
                Path = "/assets/skills/Knex.js.svg",
                Keywords = new[] { "Knex.js", "JavaScript", "SQL", "Query Builder", "Database" },
            },
            new Skill
            {
                Id = "29uf16jh8e9dhpcxjudjn",
                Name = "Knockout",
                Key = "knockout",
                Path = "/assets/skills/Knockout.svg",
                Keywords = new[] { "Knockout", "JavaScript", "MVVM", "Data Binding", "Web Development" },
            },
            new Skill
            {
                Id = "216syywg218coyrtza7ku",
                Name = "Kotlin",
                Key = "kotlin",
                Path = "/assets/skills/Kotlin.svg",
                Keywords = new[] { "Kotlin", "Programming Language", "Android Development", "Java", "JVM", "Backend Development", "Functional Programming" },
            },
            new Skill
            {
                Id = "5scqmonqyvfyughvtri24",
                Name = "KrakenJS",
                Key = "krakenjs",
                Path = "/assets/skills/Krakenjs.svg",
                Keywords = new[] { "KrakenJS", "JavaScript", "Node.js", "Web Development", "Express.js" },
            },
            new Skill
            {
                Id = "84xhjgkbfnh1bbihb98hz",
                Name = "Ktor",
                Key = "ktor",
                Path = "/assets/skills/Ktor.svg",
                Keywords = new[] { "Ktor", "Kotlin", "Web Development", "Microservices", "Backend" },
            },
            new Skill
            {
                Id = "f8yuipxpcctruinmv2cvx",
                Name = "Kubernetes",
                Key = "kubernetes",
                Path = "/assets/skills/Kubernetes.svg",
                Keywords = new[] { "Kubernetes", "Container Orchestration", "DevOps", "Microservices", "Cloud Computing", "Docker", "Scalability" },
            },
            new Skill
            {
                Id = "4vteoy2mhvppcqfpeeos3",
                Name = "LabVIEW",
                Key = "labview",
                Path = "/assets/skills/LabVIEW.svg",
                Keywords = new[] { "LabVIEW", "Visual Programming", "Engineering", "Data Acquisition", "Automation" },
            },
            new Skill
            {
                Id = "s8vnb1t4e3aio4aoc1cva",
                Name = "Laravel",
                Key = "laravel",
                Path = "/assets/skills/Laravel.svg",
                Keywords = new[] { "Laravel", "PHP", "Web Development", "Backend Development", "MVC Framework", "ORM", "Blade", "Artisan" },
            },
            new Skill
            {
                Id = "1nq3fge78e8a3ptdcs3u3",
                Name = "LaTeX",
                Key = "latex",
                Path = "/assets/skills/LaTeX.svg",
                Keywords = new[] { "LaTeX", "Typesetting", "Scientific Papers", "Math Formulas", "Publishing", "Open Source", "Documents" },
            },
            new Skill
            {
                Id = "mzisnefdark9rmhmf5emt",
                Name = "Less",
                Key = "less",
                Path = "/assets/skills/Less.js.svg",
                Keywords = new[] { "Less", "CSS Preprocessor", "Frontend Development", "Web Development", "Styling", "Variables", "Mixins", "Nesting" },
            },
            new Skill
            {
                Id = "4j91jym4kpdftkdub2mk7",
                Name = "LinkedIn",
                Key = "linkedin",
                Path = "/assets/skills/LinkedIn.svg",
                Keywords = new[] { "LinkedIn", "Social Media", "Professional Networking", "Recruitment", "Business" },
            },
            new Skill
            {
                Id = "264uxwtigs93kppncqyxs",
                Name = "Linux",
                Key = "linux",
                Path = "/assets/skills/Linux.svg",
                Keywords = new[] { "Linux", "Operating System", "Open Source", "Kernel", "Server", "UNIX", "System Administration", "Command Line" },
            },
            new Skill
            {
                Id = "wnau57wwr649rsy3rp3xv",
                Name = "Liquibase",
                Key = "liquibase",
                Path = "/assets/skills/Liquibase.svg",
                Keywords = new[] { "Liquibase", "Database Migration", "Version Control", "DevOps", "Automation" },
            },
            new Skill
            {
                Id = "rp6w3x2k61xdoqam6grei",
                Name = "Livewire",
                Key = "livewire",
                Path = "/assets/skills/Livewire.svg",
                Keywords = new[] { "Livewire", "PHP", "Laravel", "Reactive Programming", "Web Development" },
            },
            new Skill
            {
                Id = "g9vmv8qe1xnpuibhvp8s4",
                Name = "LLVM",
                Key = "llvm",
                Path = "/assets/skills/LLVM.svg",
                Keywords = new[] { "LLVM", "Compiler Infrastructure", "Toolchain", "Low-Level", "Programming Languages" },
            },
            new Skill
            {
                Id = "dbmqpimabn4x3ope6bhki",
                Name = "Logstash",
                Key = "logstash",
                Path = "/assets/skills/Logstash.svg",
                Keywords = new[] { "Logstash", "Elastic Stack", "Data Processing", "Log Analysis", "Monitoring" },
            },
            new Skill
            {
                Id = "874vfzghpf9if7rie7wx6",
                Name = "Lua",
                Key = "lua",
                Path = "/assets/skills/Lua.svg",
                Keywords = new[] { "Lua", "Scripting Language", "Game Development", "Embedded Systems", "Lightweight", "Interpreted", "Cross-Platform" },
            },
            new Skill
            {
                Id = "w5xyu7jb87ay1qzhs68nn",
                Name = "Lumen",
                Key = "lumen",
                Path = "/assets/skills/Lumen.svg",
                Keywords = new[] { "Lumen", "PHP", "Microservices", "Web Development", "Laravel" },
            },
            new Skill
            {
                Id = "6ow6cooawfs65u41ijnc8",
                Name = "Markdown",
                Key = "markdown",
                Path = "/assets/skills/Markdown.svg",
                Keywords = new[] { "Markdown", "Markup Language", "Documentation", "Writing", "Plain Text", "Formatting", "GitHub" },
            },
            new Skill
            {
                Id = "z2oxrgm8933x7f9zsxrmk",
                Name = "Material UI",
                Key = "material-ui",
                Path = "/assets/skills/Material-UI.svg",
                Keywords = new[] { "Material UI", "React", "UI Framework", "Design System", "Web Development" },
            },
            new Skill
            {
                Id = "9rt4aw5gxufyokfydz586",
                Name = "Materialize",
                Key = "materialize",
                Path = "/assets/skills/Materialize.svg",
                Keywords = new[] { "Materialize", "CSS Framework", "Responsive Design", "Frontend", "Web Development" },
            },
            new Skill
            {
                Id = "b28hzh77n6mv5g2bp2jrb",
                Name = "MATLAB",
                Key = "matlab",
                Path = "/assets/skills/MATLAB.svg",
                Keywords = new[] { "MATLAB", "Mathematics", "Data Analysis", "Simulation", "Engineering", "Programming Language", "MathWorks" },
            },
            new Skill
            {
                Id = "732ujreniuyo4yj5qeq24",
                Name = "Matplotlib",
                Key = "matplotlib",
                Path = "/assets/skills/Matplotlib.svg",
                Keywords = new[] { "Matplotlib", "Python", "Data Visualization", "Graphs", "Charts" },
            },
            new Skill
            {
                Id = "k784etweh23xeame99hqw",
                Name = "Maven",
                Key = "maven",
                Path = "/assets/skills/Apache-Maven.svg",
                Keywords = new[] { "Maven", "Build Automation", "Java", "Dependency Management", "CI/CD", "DevOps", "POM", "Project Management" },
            },
            new Skill
            {
                Id = "8wntfgbz7xhdya4y7gicj",
                Name = "Meteor",
                Key = "meteor",
                Path = "/assets/skills/Meteor.svg",
                Keywords = new[] { "Meteor", "JavaScript", "Full-Stack", "Realtime Web Apps", "Node.js", "MongoDB", "Frontend Development" },
            },
            new Skill
            {
                Id = "xbnxvvdj3qtkkg87f2qoh",
                Name = "Meteor.js",
                Key = "meteor-js",
                Path = "/assets/skills/Meteor.js.svg",
                Keywords = new[] { "Meteor.js", "JavaScript", "Full-Stack", "Web Development", "Real-Time" },
            },
            new Skill
            {
                Id = "77yktdqvzuyiqhjt4ugk9",
                Name = "Microsoft SQL Server",
                Key = "microsoft-sql-server",
                Path = "/assets/skills/Microsoft-SQL-Server.svg",
                Keywords = new[] { "Microsoft SQL Server", "Database", "RDBMS", "Microsoft", "Data Management" },
            },
            new Skill
            {
                Id = "wdib6eampmie726879q2d",
                Name = "Minitab",
                Key = "minitab",
                Path = "/assets/skills/Minitab.svg",
                Keywords = new[] { "Minitab", "Statistics", "Data Analysis", "Quality Improvement", "Six Sigma" },
            },
            new Skill
            {
                Id = "gbmpvkirdng1qs75pcspg",
                Name = "MobX",
                Key = "mobx",
                Path = "/assets/skills/MobX.svg",
                Keywords = new[] { "MobX", "State Management", "React", "JavaScript", "Frontend" },
            },
            new Skill
            {
                Id = "rfvb4abcjm91d7eo2wacw",
                Name = "Mocha",
                Key = "mocha",
                Path = "/assets/skills/Mocha.svg",
                Keywords = new[] { "Mocha", "JavaScript", "Testing", "Unit Testing", "BDD", "TDD", "Node.js" },
            },
            new Skill
            {
                Id = "295dc18py32763vrq9g8y",
                Name = "MODX",
                Key = "modx",
                Path = "/assets/skills/MODX.svg",
                Keywords = new[] { "MODX", "CMS", "PHP", "Web Development", "Open Source" },
            },
            new Skill
            {
                Id = "pykrwmoynhsbchbsj2i8s",
                Name = "Moleculer",
                Key = "moleculer",
                Path = "/assets/skills/Moleculer.svg",
                Keywords = new[] { "Moleculer", "Microservices", "JavaScript", "Node.js", "Backend" },
            },
            new Skill
            {
                Id = "tdosnumq43ec4eak99wtx",
                Name = "MongoDB",
                Key = "mongodb",
                Path = "/assets/skills/MongoDB.svg",
                Keywords = new[] { "MongoDB", "NoSQL Database", "Document Store", "JSON", "Scalability", "Replica Sets", "Sharding" },
            },
            new Skill
            {
                Id = "qxgrn69f85dymrbgweyt6",
                Name = "Mongoose",
                Key = "mongoose",
                Path = "/assets/skills/Mongoose.js.svg",
                Keywords = new[] { "Mongoose", "Node.js", "MongoDB", "Object Data Modeling", "ODM", "Schema", "Validation" },
            },
            new Skill
            {
                Id = "nnpa7yvca8iboi9r3fds7",
                Name = "Moodle",
                Key = "moodle",
                Path = "/assets/skills/Moodle.svg",
                Keywords = new[] { "Moodle", "Learning Management System", "LMS", "Education", "PHP" },
            },
            new Skill
            {
                Id = "ts9ax3mvbhfa6wm7yo5us",
                Name = "MS-DOS",
                Key = "ms-dos",
                Path = "/assets/skills/MS-DOS.svg",
                Keywords = new[] { "MS-DOS", "Operating System", "Command Line", "Legacy", "Microsoft" },
            },
            new Skill
            {
                Id = "tfrcc1oajv9kxnjcpip69",
                Name = "MySQL",
                Key = "mysql",
                Path = "/assets/skills/MySQL.svg",
                Keywords = new[] { "MySQL", "Database", "SQL", "Open Source", "Relational Database", "RDBMS", "Replication", "Scalability" },
            },
            new Skill
            {
                Id = "wkgzojsfr6znw1xbf9p7n",
                Name = "Nano",
                Key = "nano",
                Path = "/assets/skills/Nano.svg",
                Keywords = new[] { "Nano", "Text Editor", "Command Line", "Linux", "Open Source" },
            },
            new Skill
            {
                Id = "pkiy3kicw8wxse64rwhtx",
                Name = "Nerog",
                Key = "nerog",
                Path = "/assets/skills/Nerog.svg",
                Keywords = new[] { "Nerog", "Programming Language", "Systems Programming", "Low-Level", "High Performance" },
            },
            new Skill
            {
                Id = "eainy45aperc94vs6858t",
                Name = "Nest.js",
                Key = "nest-js",
                Path = "/assets/skills/Nest.js.svg",
                Keywords = new[] { "Nest.js", "Node.js", "JavaScript", "TypeScript", "Backend", "Web Development", "Microservices", "GraphQL" },
            },
            new Skill
            {
                Id = "vhu9i5j2t5qj13rp2ce8q",
                Name = "NetworkX",
                Key = "networkx",
                Path = "/assets/skills/NetworkX.svg",
                Keywords = new[] { "NetworkX", "Python", "Graph Theory", "Data Analysis", "Algorithms" },
            },
            new Skill
            {
                Id = "q4e41zgfgkmtq8mgi2j7q",
                Name = "New4j",
                Key = "new4j",
                Path = "/assets/skills/New4j.svg",
                Keywords = new[] { "New4j", "Graph Database", "NoSQL", "Data Modeling", "Cypher" },
            },
            new Skill
            {
                Id = "t8y57t1ww3955h85bmc1s",
                Name = "Next.js",
                Key = "next-js",
                Path = "/assets/skills/Next.js.svg",
                Keywords = new[]
                {
                    "Next.js",
                    "React",
                    "JavaScript",
                    "Frontend Development",
                    "Static Site Generation",
                    "Server-Side Rendering",
                    "SSR",
                    "Web Development",
                },
            },
            new Skill
            {
                Id = "7rob7imcxvgwztcrsbqt9",
                Name = "Nginx",
                Key = "nginx",
                Path = "/assets/skills/Nginx.svg",
                Keywords = new[] { "Nginx", "Web Server", "Reverse Proxy", "Load Balancing", "Caching", "Scalability", "Open Source" },
            },
            new Skill
            {
                Id = "5rsvbbo8aaov3f7cwb4wn",
                Name = "NHibernate",
                Key = "nhibernate",
                Path = "/assets/skills/NHibernate.svg",
                Keywords = new[] { "NHibernate", "ORM", "C#", "Database", "Backend" },
            },
            new Skill
            {
                Id = "c7eavoeitsvhh68a3v9c8",
                Name = "Nim",
                Key = "nim",
                Path = "/assets/skills/Nim.svg",
                Keywords = new[] { "Nim", "Programming Language", "Systems Programming", "Cross-Platform", "High Performance" },
            },
            new Skill
            {
                Id = "fcway7a6a2k3jmi8sqs5j",
                Name = "Nimble",
                Key = "nimble",
                Path = "/assets/skills/Nimble.svg",
                Keywords = new[] { "Nimble", "Package Manager", "Nim", "Dependency Management", "Open Source" },
            },
            new Skill
            {
                Id = "yr9hz8wtgk6oj4kq5r9ym",
                Name = "NixOS",
                Key = "nixos",
                Path = "/assets/skills/NixOS.svg",
                Keywords = new[] { "NixOS", "Linux", "Operating System", "Declarative Configuration", "Package Management" },
            },
            new Skill
            {
                Id = "r8xkbkbe7fbructx4qifr",
                Name = "Node.js",
                Key = "node-js",
                Path = "/assets/skills/Node.js.svg",
                Keywords = new[]
                {
                    "Node.js",
                    "JavaScript",
                    "Backend Development",
                    "Server-Side",
                    "Web Development",
                    "Event-Driven",
                    "Non-Blocking I/O",
                    "Asynchronous Programming",
                },
            },
            new Skill
            {
                Id = "mnk4yj9kfdi9zjupeqhyr",
                Name = "Nodemon",
                Key = "nodemon",
                Path = "/assets/skills/Nodemon.svg",
                Keywords = new[] { "Nodemon", "Node.js", "Development Tool", "Live Reload", "Automation", "Backend Development" },
            },
            new Skill
            {
                Id = "s7cydmb8xjv2h6psdstyc",
                Name = "NPM",
                Key = "npm",
                Path = "/assets/skills/NPM.svg",
                Keywords = new[] { "NPM", "Node.js", "Package Manager", "JavaScript", "Dependency Management", "Frontend Development", "Backend Development" },
            },
            new Skill
            {
                Id = "eduxrrerarv456385aoh2",
                Name = "NuGet",
                Key = "nuget",
                Path = "/assets/skills/NuGet.svg",
                Keywords = new[] { "NuGet", "Package Manager", ".NET", "Dependency Management", "Microsoft" },
            },
            new Skill
            {
                Id = "ie6i3zsyr6jjci9r2qvzo",
                Name = "NumPy",
                Key = "numpy",
                Path = "/assets/skills/NumPy.svg",
                Keywords = new[] { "NumPy", "Python", "Numerical Computing", "Data Analysis", "Scientific Computing" },
            },
            new Skill
            {
                Id = "2ntoq2xxn4th2rgde8aq3",
                Name = "Nuxt.js",
                Key = "nuxt-js",
                Path = "/assets/skills/Nuxt.js.svg",
                Keywords = new[]
                {
                    "Nuxt.js",
                    "Vue.js",
                    "JavaScript",
                    "Frontend Development",
                    "Static Site Generation",
                    "Server-Side Rendering",
                    "SSR",
                    "Web Development",
                },
            },
            new Skill
            {
                Id = "yhzpwwe7xnrfpergsmamt",
                Name = "NW.js (node-webkit)",
                Key = "nw-js",
                Path = "/assets/skills/NW.js-(node-webkit).svg",
                Keywords = new[] { "NW.js", "Node.js", "JavaScript", "Desktop Apps", "Web Technologies" },
            },
            new Skill
            {
                Id = "a377de66geishmu3sfrct",
                Name = "Objective-C",
                Key = "objective-c",
                Path = "/assets/skills/Objective-C.svg",
                Keywords = new[] { "Objective-C", "Programming Language", "Apple", "iOS", "MacOS" },
            },
            new Skill
            {
                Id = "aqom67wnqkxmhxazhii58",
                Name = "OCaml",
                Key = "ocaml",
                Path = "/assets/skills/OCaml.svg",
                Keywords = new[] { "OCaml", "Programming Language", "Functional Programming", "Type Safety", "Concurrency", "Pattern Matching" },
            },
            new Skill
            {
                Id = "wrb7krff1xaoy181zfrq7",
                Name = "Oh my zsh",
                Key = "oh-my-zsh",
                Path = "/assets/skills/Oh-my-zsh.svg",
                Keywords = new[] { "Oh my zsh", "Zsh", "Shell", "Command Line", "Linux" },
            },
            new Skill
            {
                Id = "3ap6b6sbkz73z35kk45hy",
                Name = "Okta",
                Key = "okta",
                Path = "/assets/skills/Okta.svg",
                Keywords = new[] { "Okta", "Identity Management", "Authentication", "Single Sign-On", "Security" },
            },
            new Skill
            {
                Id = "kxz9qi2fa1hz5p1xbvgf2",
                Name = "OpenAL",
                Key = "openal",
                Path = "/assets/skills/OpenAL.svg",
                Keywords = new[] { "OpenAL", "Audio Library", "3D Sound", "Cross-Platform", "Open Source" },
            },
            new Skill
            {
                Id = "in4n11tcsf9ffxqz2q55a",
                Name = "OpenAPI",
                Key = "openapi",
                Path = "/assets/skills/OpenAPI.svg",
                Keywords = new[] { "OpenAPI", "API Documentation", "RESTful APIs", "Swagger", "Web Development" },
            },
            new Skill
            {
                Id = "8mpzmd8zsgzpyepurtvq2",
                Name = "OpenCL",
                Key = "opencl",
                Path = "/assets/skills/OpenCL.svg",
                Keywords = new[] { "OpenCL", "Parallel Computing", "GPU", "Heterogeneous Computing", "Open Source" },
            },
            new Skill
            {
                Id = "ikni5prcqe42twfokserz",
                Name = "OpenCV",
                Key = "opencv",
                Path = "/assets/skills/OpenCV.svg",
                Keywords = new[] { "OpenCV", "Computer Vision", "Image Processing", "Machine Learning", "Python", "C++", "Artificial Intelligence" },
            },
            new Skill
            {
                Id = "ddrwaxw6vszyc4p2vhmwt",
                Name = "OpenGL",
                Key = "opengl",
                Path = "/assets/skills/OpenGL.svg",
                Keywords = new[] { "OpenGL", "Graphics Programming", "3D Graphics", "Rendering", "Shaders", "Game Development", "C++", "Cross-Platform" },
            },
            new Skill
            {
                Id = "6o23ny98gohmd82huw72c",
                Name = "OpenStack",
                Key = "openstack",
                Path = "/assets/skills/OpenStack.svg",
                Keywords = new[] { "OpenStack", "Cloud Computing", "IaaS", "Infrastructure", "Open Source", "Virtualization", "Networking" },
            },
            new Skill
            {
                Id = "26i4p7k9yjd324koubn22",
                Name = "openSUSE",
                Key = "opensuse",
                Path = "/assets/skills/openSUSE.svg",
                Keywords = new[] { "openSUSE", "Linux", "Operating System", "Open Source", "Enterprise" },
            },
            new Skill
            {
                Id = "x1p8p8dk1in6kreekhzix",
                Name = "OpenTelemetry",
                Key = "opentelemetry",
                Path = "/assets/skills/OpenTelemetry.svg",
                Keywords = new[] { "OpenTelemetry", "Observability", "Tracing", "Metrics", "Monitoring", "Distributed Systems", "Open Source", "Telemetry" },
            },
            new Skill
            {
                Id = "h7ckx9dnn6mrdur5kat7t",
                Name = "Opera",
                Key = "opera",
                Path = "/assets/skills/Opera.svg",
                Keywords = new[] { "Opera", "Web Browser", "Internet", "Cross-Platform", "Mobile Browsing" },
            },
            new Skill
            {
                Id = "tecxtfyeecb8tx23skydu",
                Name = "Oracle",
                Key = "oracle",
                Path = "/assets/skills/Oracle.svg",
                Keywords = new[] { "Oracle", "Database", "RDBMS", "Enterprise", "Cloud Computing" },
            },
            new Skill
            {
                Id = "g3rgogeya5qhj9e2u4eg6",
                Name = "p5 JS",
                Key = "p5-js",
                Path = "/assets/skills/p5-JS.svg",
                Keywords = new[] { "p5 JS", "JavaScript", "Creative Coding", "Visual Arts", "Web Development" },
            },
            new Skill
            {
                Id = "mk676fmoqeyrr324j7n84",
                Name = "Packer",
                Key = "packer",
                Path = "/assets/skills/Packer.svg",
                Keywords = new[] { "Packer", "Infrastructure as Code", "Automation", "DevOps", "HashiCorp" },
            },
            new Skill
            {
                Id = "e5jio528791hekroi1nwb",
                Name = "Pandas",
                Key = "pandas",
                Path = "/assets/skills/Pandas.svg",
                Keywords = new[] { "Pandas", "Python", "Data Analysis", "Data Science", "DataFrame", "Machine Learning", "ETL", "Jupyter Notebooks" },
            },
            new Skill
            {
                Id = "srfyhwpod9zcm8byzxs7d",
                Name = "Perl",
                Key = "perl",
                Path = "/assets/skills/Perl.svg",
                Keywords = new[] { "Perl", "Scripting Language", "Text Processing", "Regular Expressions", "System Administration", "Web Development" },
            },
            new Skill
            {
                Id = "znucedp3t1apkw1q3356e",
                Name = "pfSense",
                Key = "pfsense",
                Path = "/assets/skills/pfSense.svg",
                Keywords = new[] { "pfSense", "Firewall", "Router", "Networking", "Open Source" },
            },
            new Skill
            {
                Id = "mbkif4noqbefiovs4dmwv",
                Name = "Phalcon",
                Key = "phalcon",
                Path = "/assets/skills/Phalcon.svg",
                Keywords = new[] { "Phalcon", "PHP", "Web Development", "MVC Framework", "Backend Development", "High Performance", "ORM" },
            },
            new Skill
            {
                Id = "adubcv5utjgt7p2mk8q6x",
                Name = "Phoenix",
                Key = "phoenix",
                Path = "/assets/skills/Phoenix.svg",
                Keywords = new[] { "Phoenix", "Elixir", "Web Development", "Backend Development", "Real-Time Web Apps", "Channels", "Scalability" },
            },
            new Skill
            {
                Id = "ojsp1x73xeajjfu9pdrsm",
                Name = "Photon Engine",
                Key = "photon-engine",
                Path = "/assets/skills/Photon-Engine-.svg",
                Keywords = new[] { "Photon Engine", "Multiplayer", "Game Development", "Real-Time", "Networking" },
            },
            new Skill
            {
                Id = "j449iwupwidcnm7qwww2y",
                Name = "PHP",
                Key = "php",
                Path = "/assets/skills/PHP.svg",
                Keywords = new[] { "PHP", "Web Development", "Backend Development", "Server-Side", "Open Source", "Scripting Language" },
            },
            new Skill
            {
                Id = "zkcwbzqcnhivdsmav2vo2",
                Name = "PhpStorm",
                Key = "phpstorm",
                Path = "/assets/skills/PhpStorm.svg",
                Keywords = new[] { "PhpStorm", "JetBrains", "IDE", "PHP", "Web Development" },
            },
            new Skill
            {
                Id = "6c4bg1srxq2fawzqvkdax",
                Name = "Playwrite",
                Key = "playwrite",
                Path = "/assets/skills/Playwrite.svg",
                Keywords = new[] { "Playwrite", "JavaScript", "Testing", "E2E Testing", "Automation" },
            },
            new Skill
            {
                Id = "xen3pzppdfnzxky6een9o",
                Name = "Plotly",
                Key = "plotly",
                Path = "/assets/skills/Ploty.svg",
                Keywords = new[] { "Plotly", "Data Visualization", "Python", "Interactive Charts", "Graphs" },
            },
            new Skill
            {
                Id = "a27xw6a2x29kauexgu1f9",
                Name = "Podman",
                Key = "podman",
                Path = "/assets/skills/Podman.svg",
                Keywords = new[] { "Podman", "Containers", "Docker", "Kubernetes", "DevOps" },
            },
            new Skill
            {
                Id = "pdsxqwo2pkzimsxvu4e2o",
                Name = "Polygon",
                Key = "polygon",
                Path = "/assets/skills/Polygon.svg",
                Keywords = new[] { "Polygon", "Blockchain", "Cryptocurrency", "Ethereum", "Decentralization" },
            },
            new Skill
            {
                Id = "1d7vvmbkqtcy7wcvbcgez",
                Name = "Portainer",
                Key = "portainer",
                Path = "/assets/skills/Portainer.svg",
                Keywords = new[] { "Portainer", "Containers", "Docker", "Kubernetes", "DevOps" },
            },
            new Skill
            {
                Id = "uq3d9xvvp53v8f1w3xu19",
                Name = "PostCSS",
                Key = "postcss",
                Path = "/assets/skills/PostCSS.svg",
                Keywords = new[] { "PostCSS", "CSS", "JavaScript", "Frontend", "Styling" },
            },
            new Skill
            {
                Id = "85kpcgu74ywvm8cfp8xzg",
                Name = "PostgreSQL",
                Key = "postgresql",
                Path = "/assets/skills/PostgreSQL.svg",
                Keywords = new[] { "PostgreSQL", "Database", "SQL", "Open Source", "Relational Database", "RDBMS", "Scalability" },
            },
            new Skill
            {
                Id = "fhzwmbhqof6rpo1hxb968",
                Name = "Postman",
                Key = "postman",
                Path = "/assets/skills/Postman.svg",
                Keywords = new[] { "Postman", "API Testing", "Development", "Automation", "RESTful APIs" },
            },
            new Skill
            {
                Id = "n2qk8xio7phwarh94wnir",
                Name = "PowerShell",
                Key = "powershell",
                Path = "/assets/skills/PowerShell.svg",
                Keywords = new[] { "PowerShell", "Scripting", "Automation", "Windows", "CLI", "System Administration", "Microsoft" },
            },
            new Skill
            {
                Id = "wf4xy3pqyapu14fqjcpbc",
                Name = "Processing",
                Key = "processing",
                Path = "/assets/skills/Processing.svg",
                Keywords = new[] { "Processing", "Creative Coding", "Visual Arts", "Programming", "Open Source" },
            },
            new Skill
            {
                Id = "tyjpgsi7a25cf16hs3gxz",
                Name = "Prometheus",
                Key = "prometheus",
                Path = "/assets/skills/Prometheus.svg",
                Keywords = new[] { "Prometheus", "Monitoring", "Metrics", "Alerting", "Open Source" },
            },
            new Skill
            {
                Id = "amqsgn29skmnhbhgjd2xv",
                Name = "Protractor Test",
                Key = "protractor-test",
                Path = "/assets/skills/Protractor-Test.svg",
                Keywords = new[] { "Protractor Test", "JavaScript", "Testing", "E2E Testing", "Web Development" },
            },
            new Skill
            {
                Id = "tg4ndo1oxufu6nwbc8pvd",
                Name = "PureScript",
                Key = "purescript",
                Path = "/assets/skills/PureScript.svg",
                Keywords = new[] { "PureScript", "Programming Language", "JavaScript", "Functional Programming", "Web Development" },
            },
            new Skill
            {
                Id = "w4akr7hojuykzaxmz8i37",
                Name = "PuTTY",
                Key = "putty",
                Path = "/assets/skills/PuTTY.svg",
                Keywords = new[] { "PuTTY", "SSH", "Terminal", "Remote Access", "Networking" },
            },
            new Skill
            {
                Id = "nup7c3h9u9ixktha4ff33",
                Name = "PyCharm",
                Key = "pycharm",
                Path = "/assets/skills/PyCharm.svg",
                Keywords = new[] { "PyCharm", "JetBrains", "IDE", "Python", "Development Environment" },
            },
            new Skill
            {
                Id = "gdzv74mmh1m3qtod8akhx",
                Name = "PyScript",
                Key = "pyscript",
                Path = "/assets/skills/PyScript.svg",
                Keywords = new[] { "PyScript", "Python", "Web Development", "Frontend", "Programming" },
            },
            new Skill
            {
                Id = "9k6v8c3uqk9se3neu79eu",
                Name = "pytest",
                Key = "pytest",
                Path = "/assets/skills/pytest.svg",
                Keywords = new[] { "pytest", "Python", "Testing", "Unit Testing", "Automation" },
            },
            new Skill
            {
                Id = "sej79kmor1wfqh4u4dbtk",
                Name = "Python",
                Key = "python",
                Path = "/assets/skills/Python.svg",
                Keywords = new[] { "Python", "Programming Language", "Data Science", "Machine Learning", "Web Development", "Automation", "Scripting" },
            },
            new Skill
            {
                Id = "trsjzdzst9sgkeuhcubfe",
                Name = "Python Poetry",
                Key = "python-poetry",
                Path = "/assets/skills/Python-Poetry.svg",
                Keywords = new[] { "Python Poetry", "Dependency Management", "Python", "Package Manager", "Open Source" },
            },
            new Skill
            {
                Id = "gvghjtwo1e4ep577f9iye",
                Name = "PyTorch",
                Key = "pytorch",
                Path = "/assets/skills/PyTorch.svg",
                Keywords = new[] { "PyTorch", "Machine Learning", "Deep Learning", "Python", "Neural Networks" },
            },
            new Skill
            {
                Id = "rq2i26yvt22met1qm4if7",
                Name = "Qodana",
                Key = "qodana",
                Path = "/assets/skills/Qodana.svg",
                Keywords = new[] { "Qodana", "Code Quality", "JetBrains", "Static Analysis", "CI/CD" },
            },
            new Skill
            {
                Id = "ufzsc7cabhqo6vfhg9bhi",
                Name = "Qt",
                Key = "qt",
                Path = "/assets/skills/Qt.svg",
                Keywords = new[] { "Qt", "C++", "Cross-Platform", "UI Development", "Embedded Systems", "Mobile Development" },
            },
            new Skill
            {
                Id = "5um22m2bhkqfcbskuo2ey",
                Name = "Quarkus",
                Key = "quarkus",
                Path = "/assets/skills/Quarkus.svg",
                Keywords = new[] { "Quarkus", "Java", "Microservices", "Cloud-Native", "Kubernetes", "Reactive Programming" },
            },
            new Skill
            {
                Id = "myc12hwryvbuc9vn1dyft",
                Name = "Quasar",
                Key = "quasar",
                Path = "/assets/skills/Quasar.svg",
                Keywords = new[] { "Quasar", "Vue.js", "JavaScript", "Frontend Development", "Web Development", "UI Framework" },
            },
            new Skill
            {
                Id = "tnaf5ipo41wy5tmyo8zg2",
                Name = "Qwik",
                Key = "qwik",
                Path = "/assets/skills/Qwik.svg",
                Keywords = new[] { "Qwik", "JavaScript", "Frontend Development", "Web Development", "Performance", "SEO" },
            },
            new Skill
            {
                Id = "cg3wrorgwtnfq1i4xcudm",
                Name = "R",
                Key = "r",
                Path = "/assets/skills/R-.svg",
                Keywords = new[] { "R", "Programming Language", "Data Analysis", "Statistics", "Machine Learning", "Data Science", "RStudio" },
            },
            new Skill
            {
                Id = "xwff483hhu2jfdsha3ow9",
                Name = "RabbitMQ",
                Key = "rabbitmq",
                Path = "/assets/skills/RabbitMQ.svg",
                Keywords = new[] { "RabbitMQ", "Messaging", "AMQP", "Microservices", "Distributed Systems", "Message Broker" },
            },
            new Skill
            {
                Id = "794sk925csj155cvs1vpv",
                Name = "Rancher",
                Key = "rancher",
                Path = "/assets/skills/Rancher.svg",
                Keywords = new[] { "Rancher", "Kubernetes", "Containers", "DevOps", "Cloud", "Container Orchestration" },
            },
            new Skill
            {
                Id = "t88j85oqz6c9p5no36iqu",
                Name = "Raspberry Pi",
                Key = "raspberry-pi",
                Path = "/assets/skills/Raspberry-Pi.svg",
                Keywords = new[] { "Raspberry Pi", "Embedded Systems", "IoT", "Prototyping", "Python", "Electronics" },
            },
            new Skill
            {
                Id = "wa2rvdq84jagnpng9tdc9",
                Name = "Reach",
                Key = "reach",
                Path = "/assets/skills/Reach.svg",
                Keywords = new[] { "Reach", "Smart Contracts", "Blockchain", "Web3", "DApps", "Decentralized" },
            },
            new Skill
            {
                Id = "bt658i8v4p19pif6m7s6u",
                Name = "React",
                Key = "react",
                Path = "/assets/skills/React.svg",
                Keywords = new[] { "React", "JavaScript", "Frontend Development", "Web Development", "UI Components", "Single-Page Applications", "SPAs" },
            },
            new Skill
            {
                Id = "jd34vavyfcoc2yuj23v4v",
                Name = "React Bootstrap",
                Key = "react-bootstrap",
                Path = "/assets/skills/React-Bootstrap.svg",
                Keywords = new[] { "React Bootstrap", "React", "JavaScript", "Frontend Development", "Web Development", "UI Framework" },
            },
            new Skill
            {
                Id = "48axfiinre8h3nw862cgx",
                Name = "Realm",
                Key = "realm",
                Path = "/assets/skills/Realm.svg",
                Keywords = new[] { "Realm", "Database", "Mobile Development", "Offline First", "Sync", "NoSQL" },
            },
            new Skill
            {
                Id = "2t81a6atxz28cg4e287mm",
                Name = "Red Hat",
                Key = "red-hat",
                Path = "/assets/skills/Red-Hat.svg",
                Keywords = new[] { "Red Hat", "Linux", "Open Source", "Enterprise", "Cloud", "Virtualization" },
            },
            new Skill
            {
                Id = "xacongpbw8ypmrywvt7s7",
                Name = "RedCube's epic Compiler Thingy programming (ReCT)",
                Key = "rect",
                Path = "/assets/skills/RedCube's-epic-Compiler-Thingy-programming-(ReCT).svg",
                Keywords = new[] { "ReCT", "Programming Language", "Compiler", "Education" },
            },
            new Skill
            {
                Id = "qho98gay7djuri19yew82",
                Name = "Redis",
                Key = "redis",
                Path = "/assets/skills/Redis.svg",
                Keywords = new[] { "Redis", "In-Memory Database", "NoSQL", "Caching", "Key-Value Store", "Pub/Sub" },
            },
            new Skill
            {
                Id = "m6h3x7gbxza1vrcen55vr",
                Name = "Redux",
                Key = "redux",
                Path = "/assets/skills/Redux.svg",
                Keywords = new[] { "Redux", "JavaScript", "State Management", "React", "Frontend Development", "Web Development" },
            },
            new Skill
            {
                Id = "ov57z9jy68sbiw1nvvwgz",
                Name = "Ren'Py",
                Key = "renpy",
                Path = "/assets/skills/Ren'Py.svg",
                Keywords = new[] { "Ren'Py", "Game Development", "Visual Novels", "Python", "Storytelling" },
            },
            new Skill
            {
                Id = "opck9819qgz6ffynj6w7n",
                Name = "Rider",
                Key = "rider",
                Path = "/assets/skills/Rider.svg",
                Keywords = new[] { "Rider", "JetBrains", "IDE", ".NET", "C#", "Cross-Platform" },
            },
            new Skill
            {
                Id = "42u9s7uymraovh4j7y8ek",
                Name = "Robot Operating System (ROS)",
                Key = "ros",
                Path = "/assets/skills/Robot-Operating-System-(ROS).svg",
                Keywords = new[] { "ROS", "Robotics", "Middleware", "Robotic Systems", "Python", "C++" },
            },
            new Skill
            {
                Id = "3b7euinwnwu6m35725qg1",
                Name = "RocksDB",
                Key = "rocksdb",
                Path = "/assets/skills/RocksDB.svg",
                Keywords = new[] { "RocksDB", "Database", "Key-Value Store", "NoSQL", "High Performance", "Storage" },
            },
            new Skill
            {
                Id = "w6qdnv4zmx1abhd69kttt",
                Name = "Rollup.js",
                Key = "rollup-js",
                Path = "/assets/skills/Rollup.js.svg",
                Keywords = new[] { "Rollup.js", "JavaScript", "Bundler", "Frontend Development", "Web Development" },
            },
            new Skill
            {
                Id = "66aryppvu522yo8npy3xj",
                Name = "RSpec",
                Key = "rspec",
                Path = "/assets/skills/RSpec.svg",
                Keywords = new[] { "RSpec", "Ruby", "Testing", "BDD", "TDD", "Automation" },
            },
            new Skill
            {
                Id = "i5nznjsrij5jzpuvyf2pf",
                Name = "RStudio",
                Key = "rstudio",
                Path = "/assets/skills/RStudio.svg",
                Keywords = new[] { "RStudio", "R", "Data Science", "Data Analysis", "Statistics", "Machine Learning" },
            },
            new Skill
            {
                Id = "dradwcys5v578zf1zydjh",
                Name = "Ruby",
                Key = "ruby",
                Path = "/assets/skills/Ruby.svg",
                Keywords = new[] { "Ruby", "Programming Language", "Web Development", "Backend Development", "Rails", "Dynamic Typing" },
            },
            new Skill
            {
                Id = "vtqg1zho9wxh2mtmcghbw",
                Name = "Ruby on Rails",
                Key = "ruby-on-rails",
                Path = "/assets/skills/Ruby-on-Rails.svg",
                Keywords = new[] { "Ruby on Rails", "Ruby", "Web Development", "Backend Development", "MVC Framework", "Full-Stack" },
            },
            new Skill
            {
                Id = "iuwh69bmcao15o38em5as",
                Name = "RubyMine",
                Key = "rubymine",
                Path = "/assets/skills/RubyMine.svg",
                Keywords = new[] { "RubyMine", "JetBrains", "IDE", "Ruby", "Web Development", "Refactoring" },
            },
            new Skill
            {
                Id = "zja8frbd37bipu2frjiuh",
                Name = "Rust",
                Key = "rust",
                Path = "/assets/skills/Rust.svg",
                Keywords = new[] { "Rust", "Programming Language", "Systems Programming", "Concurrency", "Memory Safety", "High Performance" },
            },
            new Skill
            {
                Id = "rvwo98e3v6oq3nyytbr28",
                Name = "Salesforce",
                Key = "salesforce",
                Path = "/assets/skills/Salesforce.svg",
                Keywords = new[] { "Salesforce", "CRM", "Cloud Computing", "Sales", "Customer Relationship Management", "Automation" },
            },
            new Skill
            {
                Id = "t1m418xrvq917gxgxhbjy",
                Name = "Sanity",
                Key = "sanity",
                Path = "/assets/skills/Sanity.svg",
                Keywords = new[] { "Sanity", "CMS", "Content Management System", "Headless CMS", "Web Development", "JavaScript", "API" },
            },
            new Skill
            {
                Id = "foo4tt1xngw7mhkqowm4d",
                Name = "Sass",
                Key = "sass",
                Path = "/assets/skills/Sass.svg",
                Keywords = new[] { "Sass", "CSS Preprocessor", "Frontend Development", "Web Development", "Styling", "Variables", "Mixins" },
            },
            new Skill
            {
                Id = "t878vvw34hhjyjuvze788",
                Name = "Scala",
                Key = "scala",
                Path = "/assets/skills/Scala.svg",
                Keywords = new[] { "Scala", "Programming Language", "Functional Programming", "JVM", "Concurrency", "Big Data", "Spark" },
            },
            new Skill
            {
                Id = "54f4hgccpgc88zbd2fhfu",
                Name = "Scalingo",
                Key = "scalingo",
                Path = "/assets/skills/Scalingo.svg",
                Keywords = new[] { "Scalingo", "PaaS", "Cloud Computing", "DevOps", "Scalability", "Containers" },
            },
            new Skill
            {
                Id = "ow4osx5qop5qf3i4k61bq",
                Name = "Selenium",
                Key = "selenium",
                Path = "/assets/skills/Selenium.svg",
                Keywords = new[] { "Selenium", "Testing", "Automation", "Web Testing", "Cross-Browser", "WebDriver", "CI/CD" },
            },
            new Skill
            {
                Id = "snm1qcqv6rpqun8dk2ft2",
                Name = "Sema Software",
                Key = "sema-software",
                Path = "/assets/skills/Sema-Software.svg",
                Keywords = new[] { "Sema Software", "Static Analysis", "Code Quality", "Code Review", "CI/CD" },
            },
            new Skill
            {
                Id = "y4ct1utbr5x8vs5x895b4",
                Name = "Sequelize",
                Key = "sequelize",
                Path = "/assets/skills/Sequelize.svg",
                Keywords = new[] { "Sequelize", "ORM", "JavaScript", "Node.js", "SQL", "Database", "Backend Development" },
            },
            new Skill
            {
                Id = "hs8rqnjvkd9f4dxod5gv3",
                Name = "Shopware",
                Key = "shopware",
                Path = "/assets/skills/Shopware.svg",
                Keywords = new[] { "Shopware", "E-commerce", "PHP", "Online Store", "Open Source", "Shopping Cart" },
            },
            new Skill
            {
                Id = "f23uca2urouaotv5a8azd",
                Name = "Simple DirectMedia Layer (SDL)",
                Key = "sdl",
                Path = "/assets/skills/Simple-DirectMedia-Layer-(SDL).svg",
                Keywords = new[] { "SDL", "Game Development", "Multimedia", "Graphics", "C++", "Cross-Platform" },
            },
            new Skill
            {
                Id = "nz8uwc769e7xg4fswbyns",
                Name = "Sketch",
                Key = "sketch",
                Path = "/assets/skills/Sketch.svg",
                Keywords = new[] { "Sketch", "UI/UX Design", "Prototyping", "Vector Graphics", "Web Design", "User Interface", "User Experience" },
            },
            new Skill
            {
                Id = "9eub9hstfn1zds4fuxzac",
                Name = "Slack",
                Key = "slack",
                Path = "/assets/skills/Slack.svg",
                Keywords = new[] { "Slack", "Communication", "Collaboration", "Messaging", "Teamwork", "Workplace" },
            },
            new Skill
            {
                Id = "gg77zoh94ob9n2er1mcin",
                Name = "Socket.io",
                Key = "socket-io",
                Path = "/assets/skills/Socket.io.svg",
                Keywords = new[] { "Socket.io", "JavaScript", "Web Development", "Realtime Communication", "WebSockets", "Backend Development" },
            },
            new Skill
            {
                Id = "dmeuo2ewipuqjanu6y3mf",
                Name = "Solid.js",
                Key = "solid-js",
                Path = "/assets/skills/Solid.js.svg",
                Keywords = new[] { "Solid.js", "JavaScript", "Frontend Development", "Web Development", "UI Framework", "Reactive Programming" },
            },
            new Skill
            {
                Id = "dvk4wectdektw35dpk6r8",
                Name = "Solidity",
                Key = "solidity",
                Path = "/assets/skills/Solidity.svg",
                Keywords = new[] { "Solidity", "Smart Contracts", "Blockchain", "Ethereum", "Web3", "DApps" },
            },
            new Skill
            {
                Id = "byspghjxtec13gif8pyaz",
                Name = "SonarQube",
                Key = "sonarqube",
                Path = "/assets/skills/SonarQube.svg",
                Keywords = new[] { "SonarQube", "Static Analysis", "Code Quality", "Code Review", "CI/CD", "DevOps" },
            },
            new Skill
            {
                Id = "fvntn2ih9d3jgfcsdcuye",
                Name = "Sourcetree",
                Key = "sourcetree",
                Path = "/assets/skills/Sourcetree.svg",
                Keywords = new[] { "Sourcetree", "Version Control", "Git", "Mercurial", "Source Code Management", "SCM", "Atlassian" },
            },
            new Skill
            {
                Id = "var9mjc4zouncppkv3t1e",
                Name = "Splunk",
                Key = "splunk",
                Path = "/assets/skills/Splunk.svg",
                Keywords = new[] { "Splunk", "Log Management", "Monitoring", "Data Analysis", "Security", "SIEM" },
            },
            new Skill
            {
                Id = "tuxx9fn8v9z1doawyhych",
                Name = "Spring",
                Key = "spring",
                Path = "/assets/skills/Spring.svg",
                Keywords = new[] { "Spring", "Java", "Backend Development", "Web Development", "Microservices", "MVC Framework", "Spring Boot" },
            },
            new Skill
            {
                Id = "3ygqk7wycm69re2oqv28k",
                Name = "SQL Developer",
                Key = "sql-developer",
                Path = "/assets/skills/SQL-Developer.svg",
                Keywords = new[] { "SQL Developer", "Database", "SQL", "Oracle", "Query Optimization", "Data Modeling" },
            },
            new Skill
            {
                Id = "op29a4bgfowaz79dqujng",
                Name = "SQLAlchemy",
                Key = "sqlalchemy",
                Path = "/assets/skills/SQLAlchemy.svg",
                Keywords = new[] { "SQLAlchemy", "Python", "ORM", "Database", "Backend Development", "Data Mapping" },
            },
            new Skill
            {
                Id = "pbqmbt5ptcyaejidc745x",
                Name = "SQLite",
                Key = "sqlite",
                Path = "/assets/skills/SQLite.svg",
                Keywords = new[] { "SQLite", "Database", "SQL", "Embedded Database", "Lightweight", "Open Source" },
            },
            new Skill
            {
                Id = "drr9rsqkd96d8ahp58ct7",
                Name = "SSH",
                Key = "ssh",
                Path = "/assets/skills/SSH.svg",
                Keywords = new[] { "SSH", "Security", "Encryption", "Remote Access", "CLI", "System Administration" },
            },
            new Skill
            {
                Id = "6rdgzsomsk27be3esfovc",
                Name = "Stack Overflow",
                Key = "stack-overflow",
                Path = "/assets/skills/Stack-Overflow.svg",
                Keywords = new[] { "Stack Overflow", "Q&A", "Programming", "Community", "Web Development", "Learning" },
            },
            new Skill
            {
                Id = "1js75771viumqr9qxd5ki",
                Name = "Stata",
                Key = "stata",
                Path = "/assets/skills/Stata.svg",
                Keywords = new[] { "Stata", "Data Analysis", "Statistics", "Econometrics", "Research" },
            },
            new Skill
            {
                Id = "3ij66k8kz41rzk8dh41pc",
                Name = "Storybook",
                Key = "storybook",
                Path = "/assets/skills/Storybook.svg",
                Keywords = new[] { "Storybook", "UI Development", "Component Library", "Frontend Development", "React", "Vue", "Angular" },
            },
            new Skill
            {
                Id = "3ezkfhv1v85bcf1j2nd7r",
                Name = "Streamlit",
                Key = "streamlit",
                Path = "/assets/skills/Streamlit.svg",
                Keywords = new[] { "Streamlit", "Python", "Data Science", "Machine Learning", "Web Apps", "Interactive Visualization" },
            },
            new Skill
            {
                Id = "afsus9ugmyidnke3oi6q4",
                Name = "Stylus",
                Key = "stylus",
                Path = "/assets/skills/Stylus.svg",
                Keywords = new[] { "Stylus", "CSS Preprocessor", "Frontend Development", "Web Development", "Styling", "Variables", "Mixins" },
            },
            new Skill
            {
                Id = "zina4k6rx7u2a3zk1vvvs",
                Name = "Svelte",
                Key = "svelte",
                Path = "/assets/skills/Svelte.svg",
                Keywords = new[] { "Svelte", "JavaScript", "Frontend Development", "Web Development", "UI Framework", "Reactive Programming" },
            },
            new Skill
            {
                Id = "hhp9txdma2rejo77c9mz7",
                Name = "Swagger",
                Key = "swagger",
                Path = "/assets/skills/Swagger.svg",
                Keywords = new[] { "Swagger", "API Documentation", "OpenAPI", "RESTful API", "Backend Development" },
            },
            new Skill
            {
                Id = "rjc35mm9mtpxqa46bsdvq",
                Name = "Swift",
                Key = "swift",
                Path = "/assets/skills/Swift.svg",
                Keywords = new[] { "Swift", "Programming Language", "iOS Development", "macOS", "Apple", "Objective-C" },
            },
            new Skill
            {
                Id = "uv1cbojcofzm6zaiwrgnv",
                Name = "Symfony",
                Key = "symfony",
                Path = "/assets/skills/Symfony.svg",
                Keywords = new[] { "Symfony", "PHP", "Web Development", "Backend Development", "MVC Framework", "ORM", "Twig" },
            },
            new Skill
            {
                Id = "nwdgnntme5t267e6wjnvx",
                Name = "Tailwind CSS",
                Key = "tailwind-css",
                Path = "/assets/skills/Tailwind-CSS.svg",
                Keywords = new[] { "Tailwind CSS", "CSS Framework", "Frontend Development", "Web Development", "Utility-First", "Responsive Design" },
            },
            new Skill
            {
                Id = "995xo5wr8qhkw545k5iit",
                Name = "Tauri",
                Key = "tauri",
                Path = "/assets/skills/Tauri.svg",
                Keywords = new[] { "Tauri", "JavaScript", "Rust", "Desktop Applications", "Cross-Platform", "Web Technologies" },
            },
            new Skill
            {
                Id = "mvfqhmwqimu92xghpi9tm",
                Name = "TensorFlow",
                Key = "tensorflow",
                Path = "/assets/skills/TensorFlow.svg",
                Keywords = new[] { "TensorFlow", "Machine Learning", "AI", "Deep Learning", "Neural Networks", "Python", "Google" },
            },
            new Skill
            {
                Id = "61mikcxxadwbn1z9hydkz",
                Name = "TeX",
                Key = "tex",
                Path = "/assets/skills/TeX.svg",
                Keywords = new[] { "TeX", "Typesetting", "Scientific Papers", "Math Formulas", "Publishing", "Open Source", "Documents" },
            },
            new Skill
            {
                Id = "xy9sumr1yn6e6mhcxin79",
                Name = "The Algorithms",
                Key = "the-algorithms",
                Path = "/assets/skills/The-Algorithms.svg",
                Keywords = new[] { "The Algorithms", "Programming", "Algorithms", "Data Structures", "Learning", "Open Source" },
            },
            new Skill
            {
                Id = "frfnv71he1mpgqq8fsxgd",
                Name = "Three.js",
                Key = "three-js",
                Path = "/assets/skills/Three.js.svg",
                Keywords = new[] { "Three.js", "JavaScript", "WebGL", "3D Graphics", "Rendering", "Game Development", "Web Development" },
            },
            new Skill
            {
                Id = "5mm3grmpeupji65vi9cut",
                Name = "Titanium SDK",
                Key = "titanium-sdk",
                Path = "/assets/skills/Titanium-SDK.svg",
                Keywords = new[] { "Titanium SDK", "Mobile Development", "JavaScript", "Cross-Platform", "Appcelerator" },
            },
            new Skill
            {
                Id = "ngt7ewi6v2vmbs7z3f76k",
                Name = "TortoiseGit",
                Key = "tortoisegit",
                Path = "/assets/skills/TortoiseGit.svg",
                Keywords = new[] { "TortoiseGit", "Version Control", "Git", "Source Code Management", "SCM", "Windows" },
            },
            new Skill
            {
                Id = "y3atv1s5xswzw3sgffosp",
                Name = "Tower",
                Key = "tower",
                Path = "/assets/skills/Tower.svg",
                Keywords = new[] { "Tower", "Version Control", "Git", "Source Code Management", "SCM", "MacOS", "Windows" },
            },
            new Skill
            {
                Id = "vj6dwcgicwmy7n2jdj2h6",
                Name = "Traefik Mesh",
                Key = "traefik-mesh",
                Path = "/assets/skills/Traefik-Mesh.svg",
                Keywords = new[] { "Traefik Mesh", "Service Mesh", "Kubernetes", "Containers", "Networking", "Cloud" },
            },
            new Skill
            {
                Id = "j1n3nhzadjqa6wspnaodu",
                Name = "Traefik Proxy",
                Key = "traefik-proxy",
                Path = "/assets/skills/Traefik-Proxy.svg",
                Keywords = new[] { "Traefik Proxy", "Reverse Proxy", "Load Balancing", "Kubernetes", "Containers", "Cloud" },
            },
            new Skill
            {
                Id = "74t7x1wxpnbu7vk46q7gr",
                Name = "Travis CI",
                Key = "travis-ci",
                Path = "/assets/skills/Travis-CI.svg",
                Keywords = new[] { "Travis CI", "CI/CD", "Continuous Integration", "Continuous Delivery", "DevOps", "Automation", "Build Pipelines" },
            },
            new Skill
            {
                Id = "ocfcfms24f1xz251iimrw",
                Name = "Trello",
                Key = "trello",
                Path = "/assets/skills/Trello.svg",
                Keywords = new[] { "Trello", "Project Management", "Kanban", "Task Management", "Collaboration", "Productivity" },
            },
            new Skill
            {
                Id = "8gfevuhxzniyqk5is673n",
                Name = "Twitter",
                Key = "twitter",
                Path = "/assets/skills/Twitter.svg",
                Keywords = new[] { "Twitter", "Social Media", "Communication", "Marketing", "Microblogging" },
            },
            new Skill
            {
                Id = "ybk64gggoaxsf6fwd7987",
                Name = "TypeScript",
                Key = "typescript",
                Path = "/assets/skills/TypeScript.svg",
                Keywords = new[] { "TypeScript", "JavaScript", "Frontend Development", "Web Development", "Type Safety", "Microsoft", "Backend Development" },
            },
            new Skill
            {
                Id = "22nzaxbybbuxewd91vbhg",
                Name = "TYPO3",
                Key = "typo3",
                Path = "/assets/skills/TYPO3.svg",
                Keywords = new[] { "TYPO3", "CMS", "Content Management System", "Web Development", "PHP", "Open Source" },
            },
            new Skill
            {
                Id = "cxwxmytk5eu7stpmr9nhu",
                Name = "Ubuntu",
                Key = "ubuntu",
                Path = "/assets/skills/Ubuntu.svg",
                Keywords = new[] { "Ubuntu", "Linux", "Operating System", "Open Source", "Server", "Desktop", "Canonical" },
            },
            new Skill
            {
                Id = "2qn5mka2echeydyopm7eb",
                Name = "Unified Modeling Language (UML)",
                Key = "uml",
                Path = "/assets/skills/Unified-Modelling-Language-(UML).svg",
                Keywords = new[] { "UML", "Modeling", "Software Design", "Diagrams", "Documentation", "Architecture" },
            },
            new Skill
            {
                Id = "9v2d24mx3qb4q3zhvijyo",
                Name = "Unity",
                Key = "unity",
                Path = "/assets/skills/Unity.svg",
                Keywords = new[] { "Unity", "Game Development", "C#", "Cross-Platform", "3D Games", "2D Games", "VR" },
            },
            new Skill
            {
                Id = "uzby29tzm6xwjh6jvskvd",
                Name = "UNIX",
                Key = "unix",
                Path = "/assets/skills/UNIX.svg",
                Keywords = new[] { "UNIX", "Operating System", "Command Line", "Linux", "Shell Scripting", "System Administration" },
            },
            new Skill
            {
                Id = "xme4nfegc77z6toba3a9h",
                Name = "Unreal Engine",
                Key = "unreal-engine",
                Path = "/assets/skills/Unreal-Engine.svg",
                Keywords = new[] { "Unreal Engine", "Game Development", "C++", "3D Games", "VR", "Epic Games" },
            },
            new Skill
            {
                Id = "3ps83adfwqvjsd6j1d121",
                Name = "uWSGI",
                Key = "uwsgi",
                Path = "/assets/skills/uWSGI.svg",
                Keywords = new[] { "uWSGI", "Web Server", "Python", "Application Server", "Deployment" },
            },
            new Skill
            {
                Id = "dz9wncff1nzsoaupif3oh",
                Name = "V8",
                Key = "v8",
                Path = "/assets/skills/V8.svg",
                Keywords = new[] { "V8", "JavaScript Engine", "Google", "Chrome", "Node.js", "Web Development" },
            },
            new Skill
            {
                Id = "inui93cmjopk9i7odaagt",
                Name = "Vala",
                Key = "vala",
                Path = "/assets/skills/Vala.svg",
                Keywords = new[] { "Vala", "Programming Language", "GObject", "GNOME", "Linux", "Open Source" },
            },
            new Skill
            {
                Id = "n2rhovpkwunb2ca4twa1z",
                Name = "Vercel",
                Key = "vercel",
                Path = "/assets/skills/Vercel.svg",
                Keywords = new[] { "Vercel", "Hosting", "Frontend Development", "Web Development", "Serverless", "Static Site Generation", "Next.js" },
            },
            new Skill
            {
                Id = "5i8oe6k3wewhi9pdyhsar",
                Name = "Vim",
                Key = "vim",
                Path = "/assets/skills/Vim.svg",
                Keywords = new[] { "Vim", "Text Editor", "Command Line", "Customization", "Open Source" },
            },
            new Skill
            {
                Id = "12r47339hu9u1baf7u8tk",
                Name = "Visual Studio",
                Key = "visual-studio",
                Path = "/assets/skills/Visual-Studio.svg",
                Keywords = new[] { "Visual Studio", "IDE", "C#", "ASP.NET", "Microsoft", "Frontend Development", "Backend Development" },
            },
            new Skill
            {
                Id = "iixtb5ct6g68wj1cg6ojm",
                Name = "Visual Studio Code (VS Code)",
                Key = "vs-code",
                Path = "/assets/skills/Visual-Studio-Code-(VS-Code).svg",
                Keywords = new[] { "VS Code", "IDE", "Text Editor", "JavaScript", "Extensions", "Frontend Development", "Microsoft" },
            },
            new Skill
            {
                Id = "9y1eqat6mrf4o7v1a8dbn",
                Name = "Vite",
                Key = "vite",
                Path = "/assets/skills/Vite.svg",
                Keywords = new[] { "Vite", "JavaScript", "Frontend Development", "Web Development", "Build Tools", "Bundler" },
            },
            new Skill
            {
                Id = "qz8vymvgczc9e3uy2vr1t",
                Name = "Vite.js",
                Key = "vite-js",
                Path = "/assets/skills/Vite.js.svg",
                Keywords = new[] { "Vite.js", "JavaScript", "Frontend Development", "Web Development", "Build Tools", "Bundler" },
            },
            new Skill
            {
                Id = "3pjfxcm28upejem8e4yjt",
                Name = "vSphere",
                Key = "vsphere",
                Path = "/assets/skills/vSphere.svg",
                Keywords = new[] { "vSphere", "Virtualization", "VMware", "Cloud", "Data Center" },
            },
            new Skill
            {
                Id = "omcxy6s1raidvx3fm29f3",
                Name = "Vue Storefront",
                Key = "vue-storefront",
                Path = "/assets/skills/Vue-Storefront.svg",
                Keywords = new[] { "Vue Storefront", "Vue.js", "JavaScript", "E-commerce", "PWA", "Frontend Development", "Web Development" },
            },
            new Skill
            {
                Id = "sg5uyn7q216vo6bxfkjf3",
                Name = "Vue.js",
                Key = "vue-js",
                Path = "/assets/skills/Vue.js.svg",
                Keywords = new[] { "Vue.js", "JavaScript", "Frontend Development", "Web Development", "UI Framework", "Reactive Programming" },
            },
            new Skill
            {
                Id = "s9pp6vgghy9a91v5y9ndm",
                Name = "Vuetify",
                Key = "vuetify",
                Path = "/assets/skills/Veutify.svg",
                Keywords = new[] { "Vuetify", "Vue.js", "JavaScript", "Frontend Development", "UI Framework", "Web Development" },
            },
            new Skill
            {
                Id = "ae1jmemdveu1hmpmams2c",
                Name = "Vyper",
                Key = "vyper",
                Path = "/assets/skills/Vyper.svg",
                Keywords = new[] { "Vyper", "Smart Contracts", "Blockchain", "Ethereum", "Web3", "Python" },
            },
            new Skill
            {
                Id = "6k2b4q8vaxv8s44u27no8",
                Name = "WebAssembly",
                Key = "webassembly",
                Path = "/assets/skills/WebAssembly.svg",
                Keywords = new[] { "WebAssembly", "Web Development", "Low-Level Programming", "Performance", "Binary" },
            },
            new Skill
            {
                Id = "o8ae744346pd6wtkyz9ji",
                Name = "Webflow",
                Key = "webflow",
                Path = "/assets/skills/Webflow.svg",
                Keywords = new[] { "Webflow", "Web Design", "No-Code", "Web Development", "Responsive Design", "CMS" },
            },
            new Skill
            {
                Id = "ydcx4juujkyjypq3bsbtv",
                Name = "Weblate",
                Key = "weblate",
                Path = "/assets/skills/Weblate.svg",
                Keywords = new[] { "Weblate", "Translation", "Localization", "Open Source", "Version Control" },
            },
            new Skill
            {
                Id = "v71ssax6ymhq1u8unr39s",
                Name = "Webpack",
                Key = "webpack",
                Path = "/assets/skills/Webpack.svg",
                Keywords = new[] { "Webpack", "JavaScript", "Bundler", "Frontend Development", "Web Development", "Build Tools", "Modules" },
            },
            new Skill
            {
                Id = "ejvp7ajp65g34v68pax72",
                Name = "WebStorm",
                Key = "webstorm",
                Path = "/assets/skills/WebStorm.svg",
                Keywords = new[] { "WebStorm", "JetBrains", "IDE", "JavaScript", "Web Development", "TypeScript", "Frontend Development" },
            },
            new Skill
            {
                Id = "9i8e6p2h91cigw3kznet1",
                Name = "Windows 11",
                Key = "windows-11",
                Path = "/assets/skills/Windows-11.svg",
                Keywords = new[] { "Windows 11", "Operating System", "Microsoft", "Desktop", "UI", "Software" },
            },
            new Skill
            {
                Id = "uu43h987xaq9g7icruwy7",
                Name = "Windows 8",
                Key = "windows-8",
                Path = "/assets/skills/Windows-8.svg",
                Keywords = new[] { "Windows 8", "Operating System", "Microsoft", "Desktop", "UI", "Software" },
            },
            new Skill
            {
                Id = "dca8v42t9yrzrsrvbr889",
                Name = "WooCommerce",
                Key = "woocommerce",
                Path = "/assets/skills/WooCommerce.svg",
                Keywords = new[] { "WooCommerce", "E-commerce", "WordPress", "Online Store", "PHP", "Shopping Cart", "Extensions" },
            },
            new Skill
            {
                Id = "zsxuiug1u6eaoz64dvie6",
                Name = "WordPress",
                Key = "wordpress",
                Path = "/assets/skills/WordPress.svg",
                Keywords = new[] { "WordPress", "CMS", "Content Management System", "Web Development", "PHP", "Open Source", "Blogging" },
            },
            new Skill
            {
                Id = "my2nruxp6n8h2i224bt3r",
                Name = "Xamarin",
                Key = "xamarin",
                Path = "/assets/skills/Xamarin.svg",
                Keywords = new[] { "Xamarin", "C#", "Mobile Development", "Cross-Platform", "Microsoft", "App Development" },
            },
            new Skill
            {
                Id = "h6fq1hpngxjtdcf7b3tdq",
                Name = "Xcode",
                Key = "xcode",
                Path = "/assets/skills/Xcode.svg",
                Keywords = new[] { "Xcode", "iOS Development", "macOS", "IDE", "Swift", "Objective-C" },
            },
            new Skill
            {
                Id = "cvufgb1x68kn5nexiw6hz",
                Name = "XML",
                Key = "xml",
                Path = "/assets/skills/XML.svg",
                Keywords = new[] { "XML", "Markup Language", "Data Exchange", "Web Development", "APIs", "Configuration" },
            },
            new Skill
            {
                Id = "ox25xd7dqjgwji72o9w9o",
                Name = "YAML",
                Key = "yaml",
                Path = "/assets/skills/YAML.svg",
                Keywords = new[] { "YAML", "Configuration", "Serialization", "Data Exchange", "Human-Readable", "Automation" },
            },
            new Skill
            {
                Id = "jhzdynqskxapy5fzmexs2",
                Name = "Yarn",
                Key = "yarn",
                Path = "/assets/skills/Yarn.svg",
                Keywords = new[] { "Yarn", "JavaScript", "Package Manager", "Node.js", "Frontend Development", "Dependency Management" },
            },
            new Skill
            {
                Id = "n8ywefpcmrivceg6nv2ha",
                Name = "Yii Framework",
                Key = "yii-framework",
                Path = "/assets/skills/Yii-Framework.svg",
                Keywords = new[] { "Yii Framework", "PHP", "Web Development", "Backend Development", "MVC Framework", "ORM" },
            },
            new Skill
            {
                Id = "q3eg5ingknhtd3q6ivjvd",
                Name = "YunoHost",
                Key = "yunohost",
                Path = "/assets/skills/Yuno-Host.svg",
                Keywords = new[] { "YunoHost", "Self-Hosting", "Server", "Linux", "Open Source", "Automation" },
            },
            new Skill
            {
                Id = "jtzjfsnwzrop6z59kna8i",
                Name = "Zend Framework",
                Key = "zend-framework",
                Path = "/assets/skills/Zend-Framework.svg",
                Keywords = new[] { "Zend Framework", "PHP", "Web Development", "Backend Development", "MVC Framework", "Open Source" },
            },
            new Skill
            {
                Id = "j4ddanyvoyxtototdyxa6",
                Name = "Zig",
                Key = "zig",
                Path = "/assets/skills/Zig.svg",
                Keywords = new[] { "Zig", "Programming Language", "Systems Programming", "Memory Safety", "Concurrency", "High Performance" },
            },
        };
    }
}
