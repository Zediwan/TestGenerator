# TestGenerator

**TestGenerator** is a WPF-based utility built with **.NET 8** and **C# 12** that automatically scans a given project directory for classes and generates structured test files based on customizable settings. Designed for maintainable and scalable testing, it supports both GUI and planned CLI workflows.

---

## Features

- **Class Scanner**  
  Recursively scans selected directories for `.cs` files and identifies public classes.

- **Method Detection**  
  Uses reflection to detect public methods, supporting method filtering and signature analysis.

- **Test File Generation**  
  Automatically generates unit test files for discovered classes and methods, using your naming conventions and structure preferences.

- **Customizable Structure**  
  Define test project root, folder layout, test class naming style, and test method naming templates.

- **Log Terminal**  
  Integrated console output within the WPF UI for status updates, warnings, and logs during scanning and generation.

- **Path Selector Interface**  
  Easily select the project root, output path, or test directory using intuitive file/folder pickers.

---

- [ ] **Preview Test Output** before generation  
- [ ] **Regenerate Tests** while preserving existing content (comments, manual changes)  
- [ ] **Test Templates**: customizable code generation using templating engine (e.g. Scriban)  
- [ ] **Method Filtering Rules** (by attribute, access modifier, return type, etc.)  
- [ ] **Test Case Parameterization** for data-driven tests  
- [ ] **Command-Line Interface (CLI)** for automation and CI integration  
- [ ] **Multi-framework Support** (e.g., NUnit, xUnit, MSTest)

---

Created by Jeremy Moser
Built for productivity-minded developers who want to scale their unit testing with structure and speed.