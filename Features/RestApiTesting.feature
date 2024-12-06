Feature: Rest API Testing
  Testing CRUD operations for the booking API.

  Scenario: Get request to fetch booking details
    Given Connection to the API
    And Creation of GET request to get the list of all booking
    And Creation of GET request to fetch booking 1 details
    When Sending the request
    Then Verify that the request is successful

  Scenario: Post request to create a booking
    Given Connection to the API
    And Creation of POST request to create a booking
    When Sending the request
    Then Verify that the booking is created successfully

  Scenario: Put request to update a booking
    Given Connection to the API and obtain authentication token
    And Creation of GET request to get the list of all booking
    And Creation of PUT request to update 1 booking
    When Sending the request
    Then Verify that the booking is updated successfully

  Scenario: Patch request to update a booking
    Given Connection to the API and obtain authentication token
    And Creation of GET request to get the list of all booking
    And Creation of PATCH request to update 1 booking
    When Sending the request
    Then Verify that the booking is updated successfully

  Scenario: Delete request to delete a booking
    Given Connection to the API and obtain authentication token
    And Creation of GET request to get the list of all booking
    And Creation of DELETE request to delete 1 booking
    When Sending the request
    Then Verify that the booking is successfully deleted
