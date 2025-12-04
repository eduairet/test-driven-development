# Introduction to TDD

- Test Driven Development (TDD) is a software development approach where tests are written before the actual code.
- The main idea is to create small, incremental tests that define the desired functionality of the code.

## Units of Work in TDD

- **Red**: Write a failing test that defines a function or improvement.
- **Green**: Write the minimum amount of code necessary to make the test pass.
- **Refactor**: Clean up the code while ensuring that all tests still pass.
- Repeat this cycle for each new piece of functionality.
- TDD emphasizes small, manageable units of work to ensure code quality and maintainability.

## Unit Tests

- Unit tests are automated tests that verify the functionality of a specific section of code, typically at the function or method level.
- Arrange-Act-Assert (AAA) pattern is commonly used in unit tests:
  - **Arrange**: Set up the necessary preconditions and inputs.
  - **Act**: Execute the code being tested.
  - **Assert**: Verify that the outcome is as expected.
