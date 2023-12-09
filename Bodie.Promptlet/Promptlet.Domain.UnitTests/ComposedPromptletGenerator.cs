using Promptlet.Domain.UnitTests;
using Promptlet.Infrastructure.Models;

namespace Promptlet.Domain.UnitTests
{
    public class ComposedPromptletGenerator
    {
        private static Random StaticRandom = new();

        public static ComposedPromptlet[] GenerateComposedPromptlets(int composedPromptletCount, int promptletArtifactCountPerComposedPromptlet, bool randomizedVariableDelimeters=false)
        {
            var composedPromptlets = new ComposedPromptlet[composedPromptletCount];

            for (int i = 0; i < composedPromptletCount; i++)
            {
                composedPromptlets[i] = new ComposedPromptlet
                {
                    ComposedPromptletId = StaticRandom.Next(1, 1001),
                    ComposedPromptletName = GetRandomComposedPromptletName(),
                    ComposedPromptletDescription = GetRandomComposedPromptletDescription(),
                    ComposedPromptletHeader = GetRandomComposedPromptletHeader(),
                    ComposedPromptletFooter = GetRandomComposedPromptletFooter(),
                    PromptletArtifacts = PromptletArtifactGenerator.GeneratePromptletArtifactsWithRandomizedDelimeters(promptletArtifactCountPerComposedPromptlet, randomizedVariableDelimeters)
                };
            }

            return composedPromptlets;
        }

        private static string GetRandomComposedPromptletName()
        {
            var names = new string[] {
                   "Analyzing Code Smells"
                , "Identifying Design Flaws"
                , "Detecting Performance Bottlenecks"
                , "Analyzing Data Pipelines"
                , "Optimizing Data Models"
                , "Improving Data Visualization"
                , "Developing Machine Learning Models"
                , "Tuning Machine Learning Hyperparameters"
                , "Evaluating Machine Learning Models"
                , "Deploying Machine Learning Models"
                , "Analyzing User Behavior"
                , "Personalizing User Experiences"
                , "Optimizing Marketing Campaigns"
                , "Improving Customer Service"
                , "Enhancing Product Recommendations"
                , "Automating Business Processes"
                , "Managing Risks and Compliance"
                , "Ensuring Data Privacy and Security" };

            return names[StaticRandom.Next(names.Length)];
        }

        private static string GetRandomComposedPromptletArtifactContent()
        {
            var descriptions = new string[] {
            "Analyze the given [language] code for code smells and suggest improvements: [code snippet]",
            "Identify design flaws in the given [language] code and suggest improvements: [code snippet]",
            "Detect performance bottlenecks in the given [language] code and suggest optimizations: [code snippet]",
            "Analyze the given data pipeline for inefficiencies and suggest improvements: [data pipeline diagram]",
            "Optimize the given data models for performance and scalability: [data model diagram]",
            "Improve the visual representation of the given data to enhance insights: [data visualization]",
            "Develop machine learning models to predict customer behavior: [data description]",
            "Tune the hyperparameters of the given machine learning model to improve performance: [model description]",
            "Evaluate the performance of the given machine learning model on a test dataset: [model description, test dataset]",
            "Deploy the given machine learning model to production: [model description, deployment environment]",
            "Analyze user behavior data to identify patterns and trends: [user behavior data]",
            "Personalize user experiences based on individual preferences: [user profile data]",
            "Optimize marketing campaigns based on customer data and behavior: [marketing campaign data, customer data]",
            "Improve customer service by analyzing customer feedback and interactions: [customer feedback data, interaction data]",
            "Enhance product recommendations based on customer purchase history and browsing behavior: [purchase history data, browsing behavior data]",
            "Automate business processes to improve efficiency and reduce costs: [business process description]",
            "Manage risks and ensure compliance with regulatory requirements: [risk assessment, regulatory requirements]",
            "Ensure data privacy and security by implementing appropriate safeguards: [data privacy policy, security measures]" };

            return descriptions[StaticRandom.Next(descriptions.Length)];
        }

        private static string GetRandomComposedPromptletDescription()
        {
            var headers = new string[] {
                "Analyzing Code Smells and Identifying Design Flaws in Python Code",
                "Detecting Performance Bottlenecks in Java Code",
                "Optimizing Data Models for Performance and Scalability in SQL Server",
                "Enhancing Data Visualization for Improved Insights in Tableau",
                "Developing Machine Learning Models to Predict Customer Behavior in Python",
                "Tuning Machine Learning Hyperparameters for Optimal Performance in R",
                "Evaluating Machine Learning Models on Test Datasets in Python",
                "Deploying Machine Learning Models to Production Environments in AWS",
                "Analyzing User Behavior Data to Identify Patterns and Trends in Google Analytics",
                "Personalizing User Experiences Based on Individual Preferences in ReactJS",
                "Optimizing Marketing Campaigns with Customer Data and Behavior in Salesforce",
                "Improving Customer Service by Analyzing Feedback and Interactions in Zendesk",
                "Enhancing Product Recommendations Based on Purchase History and Browsing Behavior in Shopify",
                "Automating Business Processes with Robotic Process Automation (RPA)",
                "Managing Risks and Ensuring Compliance with Regulatory Requirements in Oracle ERP",
                "Implementing Data Privacy and Security Safeguards in SAP",
                "Analyzing Social Media Trends to Understand Customer Sentiment and Insights",
                "Identifying Potential Security Vulnerabilities in Web Applications",
                "Optimizing Cloud Infrastructure for Cost-Effectiveness and Performance",
                "Enhancing Cybersecurity Measures to Protect Against Cyberattacks",
                "Improving DevOps Practices for Continuous Delivery and Integration",
                "Enhancing Data Governance and Data Quality for Reliable Decision-Making",
                "Exploring the Potential of Artificial Intelligence (AI) in Business Applications",
                "Leveraging Machine Learning for Predictive Analytics and Forecasting",
                "Harnessing the Power of Natural Language Processing (NLP) for Text Analytics",
                "Implementing Chatbots and Virtual Assistants for Enhanced Customer Experience",
                "Utilizing Augmented Reality (AR) and Virtual Reality (VR) for Immersive Experiences",
                "Applying Blockchain Technology for Secure and Transparent Transactions",
                "Adopting Agile Methodologies for Project Management and Delivery",
                "Fostering a Culture of Innovation and Continuous Learning in Organizations",
                "Preparing for the Future of Work with Emerging Technologies",
                "Exploring Ethical Considerations and Responsible AI Practices" };

            return headers[StaticRandom.Next(headers.Length)];
        }

        private static string GetRandomComposedPromptletHeader()
        {
            var footers = new string[] {
                "Analyzing Code Smells and Identifying Design Flaws in Python Code",
                "Detecting Performance Bottlenecks in Java Code",
                "Optimizing Data Models for Performance and Scalability in SQL Server",
                "Enhancing Data Visualization for Improved Insights in Tableau",
                "Developing Machine Learning Models to Predict Customer Behavior in Python",
                "Tuning Machine Learning Hyperparameters for Optimal Performance in R",
                "Evaluating Machine Learning Models on Test Datasets in Python",
                "Deploying Machine Learning Models to Production Environments in AWS",
                "Analyzing User Behavior Data to Identify Patterns and Trends in Google Analytics",
                "Personalizing User Experiences Based on Individual Preferences in ReactJS",
                "Optimizing Marketing Campaigns with Customer Data and Behavior in Salesforce",
                "Improving Customer Service by Analyzing Feedback and Interactions in Zendesk",
                "Enhancing Product Recommendations Based on Purchase History and Browsing Behavior in Shopify",
                "Automating Business Processes with Robotic Process Automation (RPA)",
                "Managing Risks and Ensuring Compliance with Regulatory Requirements in Oracle ERP",
                "Implementing Data Privacy and Security Safeguards in SAP",
                "Analyzing Social Media Trends to Understand Customer Sentiment and Insights",
                "Identifying Potential Security Vulnerabilities in Web Applications",
                "Optimizing Cloud Infrastructure for Cost-Effectiveness and Performance",
                "Enhancing Cybersecurity Measures to Protect Against Cyberattacks",
                "Improving DevOps Practices for Continuous Delivery and Integration",
                "Enhancing Data Governance and Data Quality for Reliable Decision-Making",
                "Exploring the Potential of Artificial Intelligence (AI) in Business Applications",
                "Leveraging Machine Learning for Predictive Analytics and Forecasting",
                "Harnessing the Power of Natural Language Processing (NLP) for Text Analytics",
                "Implementing Chatbots and Virtual Assistants for Enhanced Customer Experience",
                "Utilizing Augmented Reality (AR) and Virtual Reality (VR) for Immersive Experiences",
                "Applying Blockchain Technology for Secure and Transparent Transactions",
                "Adopting Agile Methodologies for Project Management and Delivery",
                "Fostering a Culture of Innovation and Continuous Learning in Organizations",
                "Preparing for the Future of Work with Emerging Technologies",
                "Exploring Ethical Considerations and Responsible AI Practices" };

            return footers[StaticRandom.Next(footers.Length)];
        }

        private static string GetRandomComposedPromptletFooter()
        {
            var footers = new string[] {
                "Analyzing Code Smells and Identifying Design Flaws in Python Code",
                "Detecting Performance Bottlenecks in Java Code",
                "Optimizing Data Models for Performance and Scalability in SQL Server",
                "Enhancing Data Visualization for Improved Insights in Tableau",
                "Developing Machine Learning Models to Predict Customer Behavior in Python",
                "Tuning Machine Learning Hyperparameters for Optimal Performance in R",
                "Evaluating Machine Learning Models on Test Datasets in Python",
                "Deploying Machine Learning Models to Production Environments in AWS",
                "Analyzing User Behavior Data to Identify Patterns and Trends in Google Analytics",
                "Personalizing User Experiences Based on Individual Preferences in ReactJS",
                "Optimizing Marketing Campaigns with Customer Data and Behavior in Salesforce",
                "Improving Customer Service by Analyzing Feedback and Interactions in Zendesk",
                "Enhancing Product Recommendations Based on Purchase History and Browsing Behavior in Shopify",
                "Automating Business Processes with Robotic Process Automation (RPA)",
                "Managing Risks and Ensuring Compliance with Regulatory Requirements in Oracle ERP",
                "Implementing Data Privacy and Security Safeguards in SAP",
                "Analyzing Social Media Trends to Understand Customer Sentiment and Insights",
                "Identifying Potential Security Vulnerabilities in Web Applications",
                "Optimizing Cloud Infrastructure for Cost-Effectiveness and Performance",
                "Enhancing Cybersecurity Measures to Protect Against Cyberattacks",
                "Improving DevOps Practices for Continuous Delivery and Integration",
                "Enhancing Data Governance and Data Quality for Reliable Decision-Making",
                "Exploring the Potential of Artificial Intelligence (AI) in Business Applications",
                "Leveraging Machine Learning for Predictive Analytics and Forecasting",
                "Harnessing the Power of Natural Language Processing (NLP) for Text Analytics",
                "Implementing Chatbots and Virtual Assistants for Enhanced Customer Experience",
                "Utilizing Augmented Reality (AR) and Virtual Reality (VR) for Immersive Experiences",
                "Applying Blockchain Technology for Secure and Transparent Transactions",
                "Adopting Agile Methodologies for Project Management and Delivery",
                "Fostering a Culture of Innovation and Continuous Learning in Organizations",
                "Preparing for the Future of Work with Emerging Technologies",
                "Exploring Ethical Considerations and Responsible AI Practices" };

            return footers[StaticRandom.Next(footers.Length)];
        }
    }
}
