//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using TestGenerator.Core.Common.Models;
//using TestGenerator.Core.Scanning;
//using File = TestGenerator.Core.Common.Models.File;

//namespace TestGenerator.Core.Generation;

//public static class MethodGenerator
//{
//    // TODO: make generateSeparateClass a UI toggle
//    // TODO: implement a UI element to define method test content
//    public static Method? Generate(File testFile, Method sourceMethod, string prefix, string suffix,
//        bool generateSeparateClass = false, string testMethodContent = "")
//    {
//        #region Argument Checks
//        if (testFile == null) throw new ArgumentNullException(nameof(testFile));
//        if (sourceMethod == null) throw new ArgumentNullException(nameof(sourceMethod));
//        #endregion

//        #region Variable Definition
//        // TODO: implement a UI element to define method test class names if CreateSeparateClassesForMethods is enabled
//        // TODO: find namespace
//        Class? testClass = null; // class to be created
//        Method? testMethod = null; // method to be created
//        #endregion

//        // Define the method to be added
//        testMethod = GenerateTestMethod(sourceMethod, prefix, suffix, testMethodContent);

//        // Check if the Method already exists in the file
//        var m = FileScanner.FindMethod(testMethod, testFile);
//        var foundMethod = m != null;
//        if (foundMethod)
//        {
//            testMethod = m;
//            return testMethod;
//        }

//        // TODO: depending on the UI settings check if a test class for this method already exists
//        if (generateSeparateClass)
//        {
//            bool testClassExists = false;
//        }

//        // Define the class the method should be added to
//        testClass = GenerateTestClassForTestMethod(testMethod, prefix, suffix);

//        // Create a namespace and add the class
//        var namespaceDeclarationSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(testClass.Namespace))
//            .AddMembers(testClass.ClassDeclarationSyntax);

//        // Create a compilation unit and add the namespace
//        var compilationUnit = SyntaxFactory.CompilationUnit()
//            .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")))
//            .AddMembers(namespaceDeclarationSyntax);

//        // Normalize the whitespace and convert to string
//        var code = compilationUnit.NormalizeWhitespace().ToFullString();

//        // Append the generated code to the file
//        System.IO.File.AppendAllText(testFile.FullPath, code);

//        // Optionally, you can also display a message box to inform the user
//        System.Windows.MessageBox.Show($"Method '{testMethod.Name}' has been generated in {testFile.FullPath}");

//        return testMethod;
//    }

//    private static Method GenerateTestMethod(Method sourceMethod, string testMethodNamePrefix = "", string testMethodNameSuffix = "", string testMethodContent = "")
//    {
//        // Create the method name from the prefix and suffix
//        var testMethodName = string.Empty;
//        if (!string.IsNullOrEmpty(testMethodNamePrefix)) testMethodName += testMethodNamePrefix;
//        testMethodName += sourceMethod.Name;
//        if (!string.IsNullOrEmpty(testMethodNameSuffix)) testMethodName += testMethodNameSuffix;

//        var testMethodContentSyntax = SyntaxFactory.ParseStatement(testMethodContent);

//        // Define the method to be added
//        var methodDeclarationSyntax = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName("void"), testMethodName)
//            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
//            .WithBody(SyntaxFactory.Block(testMethodContentSyntax));

//        return new Method(methodDeclarationSyntax);
//    }

//    private static Class GenerateTestClassForTestMethod(Method testMethod, string testClassNamePrefix = "", string testClassNameSuffix = "")
//    {
//        // Create the class name from the prefix and suffix
//        var testClassName = string.Empty;
//        if (!string.IsNullOrEmpty(testClassNamePrefix)) testClassName += testClassNamePrefix;
//        testClassName += testMethod.Name;
//        if (!string.IsNullOrEmpty(testClassNameSuffix)) testClassName += testClassNameSuffix;

//        var classDeclarationSyntax = SyntaxFactory.ClassDeclaration(testClassName)
//            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
//            .AddMembers(testMethod.MethodDeclarationSyntax);

//        return new Class(classDeclarationSyntax);
//    }
//}