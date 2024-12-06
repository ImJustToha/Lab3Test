Feature: Rest RIOT API Testing
  Testing RiotGames API.

  Scenario: Get request to recieve User's data
    Given Creation of GET request to get Kimitsuno data with Toha tag
    When Sending the request to the riot API
    Then Verify that the riot request is successful

  Scenario: Get request to recieve summoner's data
    Given Creation of GET request to get Kimitsuno data with Toha tag
    And Creation of GET request to get summoner information
    When Sending the request to the riot API
    Then Verify that the riot request is successful

  Scenario: Get the list of free champions
    Given Creation of GET request to receive the list of free champions
    When Sending the request to the riot API
    Then Verify that the riot request is successful

  Scenario: Get the list of all champion mastery entries
    Given Creation of GET request to get Kimitsuno data with Toha tag
    And Creation of GET request to get all champion mastery entries
    When Sending the request to the riot API
    Then Verify that the riot request is successful

  Scenario: Get a champion mastery by puuid and champion ID
    Given Creation of GET request to get Kimitsuno data with Toha tag
    And Creation a GET request to get a champion 40 mastery
    When Sending the request to the riot API
    Then Verify that the riot request is successful