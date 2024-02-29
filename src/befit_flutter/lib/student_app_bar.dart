import 'package:flutter/material.dart';

class StudentAppBar extends StatelessWidget implements PreferredSizeWidget{
  final String name;
  final String email;

  const StudentAppBar({super.key, required this.name, required this.email});
  

  @override
  Size get preferredSize => const Size.fromHeight(kToolbarHeight);
  

  @override
  Widget build(BuildContext context) {    
    return AppBar(
      title: Row(
        children: [
          SafeArea(
            child: Padding(
              padding: const EdgeInsets.fromLTRB(50, 15, 15, 10),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(name, style: const TextStyle(fontSize: 20)),
                  Text(email, style: const TextStyle(fontSize: 16)),
                ],
              ),
            ),
          ),
          const Expanded(
            child: Center(
              child: Text(
                'Be-Fit testbatterij',
                style: TextStyle(fontSize: 30, fontWeight: FontWeight.bold),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
