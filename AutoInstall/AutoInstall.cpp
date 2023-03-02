#include <iostream>
#include <string>
#include <random>
#include <chrono>
#include <Windows.h>
#include <thread>
int main()
{

    SetConsoleTitleA("Runtime Install : ELX [HELLO JJ#2631]");
    for (int i = 0; i < 100; i++)
    {
        std::cout << "\r";
        std::cout << "-> Please wait... " << i << "%";
        std::this_thread::sleep_for(std::chrono::milliseconds(50));
    }
    std::cout << "\n";
    std::cout << "-> Auto Install Runtime v6\n";
    std::this_thread::sleep_for(std::chrono::milliseconds(300));
    std::cout << "-> It's will automatic insatll!\n";


    const char* url = "https://download.visualstudio.microsoft.com/download/pr/035efed3-6386-4e1d-bcbc-384a20ebf47e/abfbea2303e0ce9cb15d430314e5858f/windowsdesktop-runtime-6.0.14-win-x64.exe";
    const char* filename = "runtime.exe";
    std::string command = "curl --silent -o" + std::string(filename) + " " + std::string(url);
    system(command.c_str());

    // Install the runtime
    system(filename);

    // Delete the runtime
    std::string command2 = "del " + std::string(filename);
    system(command2.c_str());
    
    MessageBoxA(NULL, "PRESS OK AUTO EXIT PROGRAM\nกดปุ่ม OK เพื่อออกโปรแกรม", "Look at me", MB_OK | MB_ICONINFORMATION);
    ExitProcess(0);

}
