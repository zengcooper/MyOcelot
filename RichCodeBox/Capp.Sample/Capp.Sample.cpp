// Capp.Sample.cpp: 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include <string>
using namespace std;



using namespace std;
class User
{

	//私有属性
private: int id;
		 string name;

		 //公共方法或属性
public:

	//方法
	void say(int id, string name) {
		this->id = id;
		this->name = name;
		printf(name.c_str());
		
	};

};

//主函数
int main()
{
	//printf("dddddddddd");
	int i = 10;
	int j = 39;
	int k = i*j;
	string s = std::to_string(k);
	printf(s.c_str());

	User user;
	user.say(10, "lcq");
	return 0;
}