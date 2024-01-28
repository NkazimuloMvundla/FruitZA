# FruitZA.Web

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 15.2.8.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.

---

## FruitZA Backend

The FruitZA backend is built using Clean Architecture principles and utilizes Identity for authentication and authorization. It includes functionalities for creating and managing categories and products.

### Integration Tests

Two integration tests are implemented to validate the category creation and product creation functionalities.

### Running the Backend

To run the backend:
1. Set the web project as the startup project.
2. Run migrations on the project level using the following commands:

dotnet ef migrations add "Remove Tables" --project src\Infrastructure --startup-project src\Web --output-dir Data\Migrations
dotnet ef database update --project src\Infrastructure --startup-project src\Web --context ApplicationDbContext

To run the frontend:
1. Run `npm i` in the Angular project directory to install the required dependencies.
