import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'main.dart';
import 'student_app_bar.dart';
import 'studentprofile_page.dart';
import 'testselector_page.dart';

class MainMenuPage extends StatelessWidget {
  const MainMenuPage({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    var appState = context.watch<StudentAppState>();

    IconData profile = Icons.account_circle;
    IconData results = Icons.sports_score_rounded;

    return Scaffold(
      appBar: StudentAppBar(name: appState.name, email: appState.email),
      body: Center(
        child: Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            ElevatedButton(
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (context) => const StudentProfilePage()),
                );
              },
              child: Row(
                children: [
                  Icon(profile),
                  const SizedBox(width: 8), // Adjust the space between icon and text
                  const Text('Profiel'),
                ],
              ),
            ),
            const SizedBox(width: 20), // Adjust space between the 2 buttons
            ElevatedButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(builder: (context) => const TestSelectorPage()),
                  );
                },
                child: Row(
                  children: [
                    Icon(results),
                    const SizedBox(
                        width: 8), // Adjust the space between icon and text
                    const Text('Resultaten'),
                  ],
                )),
          ],
        ),
      ),
    );
  }
}
// 