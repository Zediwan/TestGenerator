# Contributing to TestGenerator

Thank you for your interest in contributing to **TestGenerator**!  
We’re excited to collaborate with you on building a faster and more structured testing workflow for C# projects.

---

## 🔧 Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022+ or Rider
- Git

### Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/Zediwan/TestGenerator.git
   cd TestGenerator
   ```
2. Switch to the development branch:
   ```bash
   git checkout dev
   ```
3. Open the solution in your IDE and build it to restore dependencies.

---

## 🔨 Workflow Overview

We work issue-first and branch-per-feature. Here’s the standard flow:

1. **Pick an issue** from the [issue tracker](https://github.com/Zediwan/TestGenerator/issues).  
   Look for those labeled `good first issue` if you’re new.

2. **Assign the issue to yourself** so others know you're working on it.

3. **Create a branch** from `dev` using the following format:
   ```bash
   git checkout -b <number>-short-description
   # example: 12-add-cli-flag
   ```

4. Implement your feature or fix. Please document public methods and classes.

5. Run and test your changes locally.

6. **Commit with context** and reference the issue:
   ```bash
   git commit -m "Add CLI flag support [Fixes #12]"
   ```

7. Push your branch:
   ```bash
   git push origin 12-add-cli-flag
   ```

8. Open a Pull Request targeting `dev`.

---

## 🧪 Tests

Unit tests live in `TestGenerator.Tests`.  
If you touch any generation or core logic, **add tests** for it.

We use conventional C# unit test structure. Future template support may integrate test output validation.

---

## 🧼 Code Style & Practices

- Use **C# conventions** and PascalCase for public elements.
- Document public classes and methods.
- Separate logic from UI where possible.
- Keep pull requests focused (1 issue per PR).

---

## 💬 Communication

- Use [GitHub Discussions](https://github.com/Zediwan/TestGenerator/discussions) for open questions, ideas, or proposals.
- If unsure, open a draft PR or ask in the discussion linked to the issue.

---

## 🙌 Thanks!

Your contributions help make this tool better for everyone.  
We’re excited to build this with you!
