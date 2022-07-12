using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetConsoleApp.DAO;
using TweetConsoleApp.Models;

namespace TweetConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to Tweet App!!");
            bool isExit = false;

            bool isLoggedin = false;
            Login userLoggedin = new Login();

            while (!isExit)
            {
                TweetService tweet = new TweetService();
                UserService user = new UserService();


                if (!isLoggedin)
                {
                m1: Console.WriteLine("You are not logged in to the application.\nPress 1 to Register\n2.Press 2 to login\n3.Press 3 to reset password.\nPress 4 to exit. ");
                    int userChoice = 0;

                    try
                    {
                        userChoice = Convert.ToInt32(Console.ReadLine());
                        switch (userChoice)
                        {
                            case 1:
                                #region User Registration
                                try
                                {
                                    Register registerModel = new Register();

                                    Console.WriteLine("Enter first Name:");
                                l1: registerModel.F_Name = Console.ReadLine();

                                    if (registerModel.F_Name == null || registerModel.F_Name == "")
                                    {
                                        Console.WriteLine("Please enter first name:");
                                        goto l1;
                                    }

                                    Console.WriteLine("Enter last name:");
                                    registerModel.L_Name = Console.ReadLine();

                                    Console.WriteLine("Enter eamil:");
                                l2: registerModel.Email = Console.ReadLine();

                                    if (registerModel.Email == null || registerModel.Email == "")
                                    {
                                        Console.WriteLine("Please enter email");
                                        goto l2;
                                    }

                                    Console.WriteLine("Enter password:");
                                l4: string tempPassword = Console.ReadLine();

                                    if (tempPassword == null || tempPassword == "")
                                    {
                                        Console.WriteLine("Please enter password");
                                        goto l4;
                                    }

                                    Console.WriteLine("Confirm password:");
                                    registerModel.Password = Console.ReadLine();

                                    if (registerModel.Password != tempPassword)
                                    {
                                        Console.WriteLine("Please enter both password same");
                                        goto l4;
                                    }

                                    Console.WriteLine("Enter Mobile no:");
                                    registerModel.Contact_No = Console.ReadLine();

                                    int userID = await user.RegisterUser(registerModel);

                                    if (userID > 0)
                                    {
                                        Console.WriteLine("You are registered successfully !!");
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Some error occured in user registration. error Message: " + e.Message);
                                    Console.WriteLine("Restarting user service");
                                    goto m1;
                                }
                                break;
                            #endregion
                            case 2:
                                #region User Login 
                                try
                                {
                                    Login userLogin = new Login();
                                    Console.WriteLine("Enter userId/Email : ");
                                l3: userLogin.Email = Console.ReadLine();

                                    if (userLogin.Email == null || userLogin.Email == "")
                                    {
                                        Console.WriteLine("Plese enter email :");
                                        goto l3;
                                    }

                                    Console.WriteLine("Enter Password:");
                                l5: userLogin.Password = Console.ReadLine();

                                    if (userLogin.Password == "")
                                    {
                                        Console.WriteLine("please enter password:");
                                        goto l5;
                                    }
                                    userLogin.isLoggedin = 1;
                                    userLogin = await user.Login(userLogin);
                                    userLoggedin = userLogin;
                                    isLoggedin = userLogin.isLoggedin == 1 && userLogin.userName != "" && userLogin.userName != null? true : false;
                                    if(!isLoggedin)
                                    {
                                        Console.WriteLine("Invalid credentials!!");
                                    }
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Some error occured in user login. Error Message: " + e.Message);
                                    Console.WriteLine("Restarting user service");
                                    goto m1;
                                }
                                break;
                            #endregion

                            case 3:
                                #region User reset password
                                try
                                {
                                    ResetPassword reset = new ResetPassword();
                                    Console.WriteLine("Enter First name:");
                                R1:  string name = Console.ReadLine();

                                    if(name == "")
                                    {
                                        Console.WriteLine("Please Enter name:");
                                        goto R1;
                                    }
                                    Console.WriteLine("Enter email:");
                                R2: reset.Email = Console.ReadLine();
                                    if (reset.Email == "")
                                    {
                                        Console.WriteLine("Please enter email:");
                                        goto R2;
                                    }
                                    Console.WriteLine("Enter new password:");
                                R3: reset.NewPassword = Console.ReadLine();
                                    if (reset.NewPassword == "")
                                    {
                                        Console.WriteLine("Please Enter new password:");
                                        goto R3;
                                    }

                                    int flag = await user.ResetPassword(name, reset);
                                    if (flag == -1)
                                    {
                                        Console.WriteLine("User/Email does not exist.");
                                    }
                                    else 
                                    {
                                        Console.WriteLine("Password reset successfully.");
                                    }
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine("Some error occured in Reset passwor service. Message : " + e.Message);
                                    Console.WriteLine("Restarting user service");
                                    goto m1;
                                }
                                break;
                                #endregion
                            case 4:
                                Console.Write("Thank you for visiting us!");
                                isExit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice, Try again:");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter valid choice!");
                        goto m1;
                    }
                }


                if(isLoggedin)
                {
                // Tweet functionality here 
                T1: Console.WriteLine("Welcome " + userLoggedin.userName + " to tweet app !! ");
                    Console.WriteLine("Press 1 to see all your tweets.\nPress 2 to post a tweet.\nPress 3 to see all tweet.");
                    Console.WriteLine("Press 4 to see all users.\nPress 5 to reset password.\nPress 6 to log out");
                    int userChoice = 0;
                    try
                    {
                        userChoice = Convert.ToInt32(Console.ReadLine());
                        Tweet tweet1 ;
                        List<Tweet> allTweets;
                        List<Users> allUsers;
                        switch (userChoice)
                        {
                            case 1:
                                tweet1 = new Tweet();
                                allTweets = await tweet.GetAllTweetSelf(userLoggedin.userName);
                                Console.WriteLine("You have total tweets: " + allTweets.Count);
                                int i = 0;
                                foreach (var tweets in allTweets)
                                {
                                    i++;
                                    Console.WriteLine("-----------Tweet " + i +" ------");
                                    Console.WriteLine("Message: "+ tweets.TweetMessage);
                                    Console.WriteLine("Date:  " + tweets.Created_Datetime);
                                    Console.WriteLine("---------------------------");
                                }
                                break;
                            case 2:
                                tweet1 = new Tweet();
                            T2: Console.WriteLine("Enter tweet message:");
                                tweet1.TweetMessage = Console.ReadLine();
                                if (tweet1.TweetMessage == null || tweet1.TweetMessage == "")
                                {
                                    Console.WriteLine("Please enter tweet message");
                                    goto T2;
                                }

                                int status = await tweet.AddTweet(tweet1, userLoggedin.userName);
                                if (status == 0)
                                {
                                    Console.WriteLine("Tweet posted successfully.");
                                }
                                else 
                                {
                                    Console.WriteLine("User not logged in properly.");
                                    isLoggedin = false;
                                }
                                break;
                            case 3:
                                allTweets = await tweet.GetAllTweets();
                                for (int j = 0; j < allTweets.Count; j++)
                                {
                                    Console.WriteLine("------ Tweet " + (j + 1) + " -------");
                                    Console.WriteLine("Tweet author: " + allTweets[j].userName);
                                    Console.WriteLine("Tweet Date: " + allTweets[j].Created_Datetime);
                                    Console.WriteLine("Tweet Message: " + allTweets[j].TweetMessage);
                                    Console.WriteLine("---------------------------------");
                                }
                                break;
                            case 4:
                                allUsers = await user.GetAllUsers();
                                for (int j = 0; j < allUsers.Count; j++)
                                {
                                    Console.WriteLine("---------------User "+ (j+1) + "--------------");
                                    Console.WriteLine("User Name:" + allUsers[j].F_Name + allUsers[j].L_Name);
                                    Console.WriteLine("User Email:" + allUsers[j].Email);
                                    Console.WriteLine("User Mobile:" + allUsers[j].Contact_No);
                                    if (j % 2 == 0 && j%3 !=0)
                                    {
                                        Console.WriteLine("User Gender: Male" );
                                    }
                                    else
                                        Console.WriteLine("User Gender: Female" );
                                    Console.WriteLine("----------------------------------");
                                }
                                break;
                            case 5:
                                ResetPassword reset = new ResetPassword();
                            T4: Console.WriteLine("Enter user Name:");
                                string userName = Console.ReadLine();

                                if (userName == "")
                                {
                                    goto T4;
                                }

                                Console.WriteLine("Enter Email:");
                                T5: reset.Email = Console.ReadLine();
                                if(reset.Email == "")
                                {
                                    Console.WriteLine("Please enter Email:");
                                    goto T5;
                                }

                                Console.WriteLine("Enter new Password:");
                            T6: reset.NewPassword = Console.ReadLine();

                                if (reset.NewPassword == "")
                                {
                                    Console.WriteLine("Please enter new password:");
                                    goto T6;
                                }

                                int flag = await user.resetPassword(userName, reset, userLoggedin.Password);
                                if (flag == 0)
                                {
                                    Console.WriteLine("Password changed !!");
                                }
                                else
                                {
                                    Console.WriteLine("Please enter proper details!");
                                }
                                break;
                            case 6:
                                userLoggedin.isLoggedin = 0;
                                userLoggedin =  await user.Login(userLoggedin);
                                if (userLoggedin.isLoggedin == 0)
                                {
                                    Console.WriteLine("log out successful.");
                                    isLoggedin = false;
                                }
                                break;
                            default:
                                Console.WriteLine("Please enter valid choice!");
                                break;
                        }                    
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Please enter valid choice!");
                        goto T1;
                    }
                }
            }
        }
    }
}
