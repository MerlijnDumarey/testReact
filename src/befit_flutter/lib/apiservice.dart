class ApiService {
  Future<Map<String, String>> fetchData() async {
    // Simulate fetching data for appbar from an API
    //returns String, String representing name and email
    await Future.delayed(const Duration(seconds: 1));
    //Simulate delay of fetching data for appbar from an API
    //replace delay with actual API call

    return {
      //default values
      'name': 'John Doe',
      'email': 'john@doe.com',
    };
  }
}
