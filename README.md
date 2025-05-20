# TestGenerator

**TestGenerator** is a WPF-based utility built with **.NET 8** and **C# 12** that automatically scans a given project directory for classes and generates structured test files based on customizable settings. Designed for maintainable and scalable testing, it supports both GUI and planned CLI workflows.

---

## 🚀 Features

### 🧠 Element Detection
The generator parses and extracts rich information from:
- **Files** → Classes
- **Classes** → Modifiers, Fields, Methods, Properties, Constructors
- **Constructors** → Modifiers, Parameters
- **Properties** → Type, Modifier, Getter/Setter
- **Methods** → Return Type, Modifiers, Parameters

---

### ✅ Current Features
- **Test File Generation**  
- **Customizable Structure**  
- **Log Terminal in UI**  
- **Path Selector Interface**

---

### 🧭 Roadmap (Planned)
- [ ] Preview Test Output before generation  
- [ ] Regenerate Tests while preserving custom code  
- [ ] Templated Test Generation (e.g. using Scriban)  
- [ ] Method Filtering (by attribute/type/access)  
- [ ] Test Case Parameterization  
- [ ] CLI Support  
- [ ] Multi-framework Support (NUnit, xUnit, MSTest)

See the full roadmap here: [📌 GitHub Project Board](https://github.com/users/Zediwan/projects/6)

---

## 🤝 Contributing

We love contributors! Here's how to get started:

### 🧭 Where to Start
- Browse the [issues](https://github.com/Zediwan/TestGenerator/issues), especially those labeled [`good first issue`](https://github.com/Zediwan/TestGenerator/issues?q=is%3Aopen+is%3Aissue+label%3A%22good+first+issue%22).
- Ask questions in the [Discussions](https://github.com/Zediwan/TestGenerator/discussions).

### 🧵 Branching Flow
1. Make sure you're on the `dev` branch.
2. Create a feature branch with the pattern:
   ```
   <number>-description
   ```
   Example:
   ```
   12-fix-null-methods
   ```

3. Mention the issue in your PR or commit:
   ```
   Fixes #12
   ```

This links the PR to the issue and closes it upon merge.

### 🧪 Testing
If your change includes core logic, add unit tests under `TestGenerator.Tests`.

---

## 📄 License
MIT

---

Created by **Jeremy Moser**  
Built for productivity-minded developers who want to scale their unit testing with structure and speed.
