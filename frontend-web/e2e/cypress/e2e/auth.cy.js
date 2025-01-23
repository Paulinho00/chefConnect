describe("Should sign in as admin and check restaurants list", () => {
  const EMAIL = "mateusz.szlachetko@gmail.com";
  const PASSWORD = "Hasło123!";

  it("logs in and navigates to Restauracje", () => {
    cy.visit(
      "http://frontend-chef-connect.s3-website-us-east-1.amazonaws.com/"
    );
    cy.wait(5000);

    cy.contains("button", "Zaloguj się").click();

    cy.get('input[name="username"]').type(EMAIL);
    cy.get('input[name="password"]').type(PASSWORD);
    cy.wait(5000);

    cy.get('button[type="submit"]').click();

    cy.wait(5000);

    cy.contains("span", "Restauracje").click();

    cy.url().should("include", "panel-administratora/restauracje");

    cy.wait(10000);

    cy.contains("button", "logout").click();
  });
});
