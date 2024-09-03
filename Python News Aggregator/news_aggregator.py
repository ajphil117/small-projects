#-----Statement of Authorship----------------------------------------#
#
#  This is an individual assessment item.  By submitting this
#  code I agree that it represents my own work.  I am aware of
#  the University rule that a student must not act in a manner
#  which constitutes academic dishonesty as stated and explained
#  in QUT's Manual of Policies and Procedures, Section C/5.3
#  "Academic Integrity" and Section E/2.1 "Student Code of Conduct".
#
#    Student no: n10477659
#    Student name: Ash Phillips
#
#  NB: Files submitted without a completed copy of this statement
#  will not be marked.  Submitted files will be subjected to
#  software plagiarism analysis using the MoSS system
#  (http://theory.stanford.edu/~aiken/moss/).
#
#--------------------------------------------------------------------#



#-----Assignment Description-----------------------------------------#
#
#  News Feed Aggregator
#
#  In this assignment you will combine your knowledge of HTMl/XML
#  mark-up languages with your skills in Python scripting, pattern
#  matching, and Graphical User Interface design to produce a useful
#  application that allows the user to aggregate RSS news feeds.
#  See the instruction sheet accompanying this file for full details.
#
#--------------------------------------------------------------------#



#-----Imported Functions---------------------------------------------#
#
# Below are various import statements for helpful functions.  You
# should be able to complete this assignment using these
# functions only.  Note that not all of these functions are
# needed to successfully complete this assignment.
#
# NB: You may NOT use any Python modules that need to be downloaded
# and installed separately, such as "Beautiful Soup" or "Pillow".
# Only modules that are part of a standard Python 3 installation may
# be used. 

# The function for opening a web document given its URL.
# (You WILL need to use this function in your solution,
# either directly or via our "download" function.)
from urllib.request import urlopen

# Import the standard Tkinter functions. (You WILL need to use
# these functions in your solution.  You may import other widgets
# from the Tkinter module provided they are ones that come bundled
# with a standard Python 3 implementation and don't have to
# be downloaded and installed separately.)
from tkinter import *

# Import a special Tkinter widget we used in our demo
# solution.  (You do NOT need to use this particular widget
# in your solution.  You may import other such widgets from the
# Tkinter module provided they are ones that come bundled
# with a standard Python 3 implementation and don't have to
# be downloaded and installed separately.)
from tkinter.scrolledtext import ScrolledText

# Functions for finding all occurrences of a pattern
# defined via a regular expression, as well as
# the "multiline" and "dotall" flags.  (You do NOT need to
# use these functions in your solution, because the problem
# can be solved with the string "find" function, but it will
# be difficult to produce a concise and robust solution
# without using regular expressions.)
from re import findall, finditer, MULTILINE, DOTALL

# Import the standard SQLite functions (just in case they're
# needed one day).
from sqlite3 import *

#
#--------------------------------------------------------------------#



#-----------------------------------------------------------
#
# A function to download and save a web document. If the
# attempted download fails, an error message is written to
# the shell window and the special value None is returned.
#
# Parameters:
# * url - The address of the web page you want to download.
# * target_filename - Name of the file to be saved (if any).
# * filename_extension - Extension for the target file, usually
#      "html" for an HTML document or "xhtml" for an XML
#      document or RSS Feed.
# * save_file - A file is saved only if this is True. WARNING:
#      The function will silently overwrite the target file
#      if it already exists!
# * char_set - The character set used by the web page, which is
#      usually Unicode UTF-8, although some web pages use other
#      character sets.
# * lying - If True the Python function will hide its identity
#      from the web server. This can be used to prevent the
#      server from blocking access to Python programs. However
#      we do NOT encourage using this option as it is both
#      unreliable and unethical!
# * got_the_message - Set this to True once you've absorbed the
#      message about Internet ethics.
#
def download(url = 'http://www.wikipedia.org/',
             target_filename = 'download',
             filename_extension = 'xhtml',
             save_file = True,
             char_set = 'UTF-8',
             lying = False,
             got_the_message = False):

    # Import the function for opening online documents and
    # the class for creating requests
    from urllib.request import urlopen, Request

    # Import an exception raised when a web server denies access
    # to a document
    from urllib.error import HTTPError

    # Open the web document for reading
    try:
        if lying:
            # Pretend to be something other than a Python
            # script (NOT RECOMMENDED!)
            request = Request(url)
            request.add_header('User-Agent', 'Mozilla/5.0')
            if not got_the_message:
                print("Warning - Request does not reveal client's true identity.")
                print("          This is both unreliable and unethical!")
                print("          Proceed at your own risk!\n")
        else:
            # Behave ethically
            request = url
        web_page = urlopen(request)
    except ValueError:
        print("Download error - Cannot find document at URL '" + url + "'\n")
        return None
    except HTTPError:
        print("Download error - Access denied to document at URL '" + url + "'\n")
        return None
    except Exception as message:
        print("Download error - Something went wrong when trying to download " + \
              "the document at URL '" + url + "'")
        print("Error message was:", message, "\n")
        return None

    # Read the contents as a character string
    try:
        web_page_contents = web_page.read().decode(char_set)
    except UnicodeDecodeError:
        print("Download error - Unable to decode document from URL '" + \
              url + "' as '" + char_set + "' characters\n")
        return None
    except Exception as message:
        print("Download error - Something went wrong when trying to decode " + \
              "the document from URL '" + url + "'")
        print("Error message was:", message, "\n")
        return None

    # Optionally write the contents to a local text file
    # (overwriting the file if it already exists!)
    if save_file:
        try:
            text_file = open(target_filename + '.' + filename_extension,
                             'w', encoding = char_set)
            text_file.write(web_page_contents)
            text_file.close()
        except Exception as message:
            print("Download error - Unable to write to file '" + \
                  target_filename + "'")
            print("Error message was:", message, "\n")

    # Return the downloaded document to the caller
    return web_page_contents

#
#--------------------------------------------------------------------#



#-----Student's Solution---------------------------------------------#
#
# Put your solution at the end of this file.
#

# Name of the exported news file. To simplify marking, your program
# should produce its results using this file name.
export_news_file_name = 'news.html'
save_database_file_name = 'news_log.db'

### Create a "seperator" used in generating the export HTML
string_seperator = ''


##### NEWS WEBSITE SOURCE CODE DATA:

### Create Constants:
max_articles = 10 # max number of articles that can be veiwed

archive_download_date = '14 October 2019'
live_date = 'Today'


### Create Custom Error Messages:
load_error_message = "Load Error - Unable to read the html file '"
download_error_message = "Download Error - Something went wrong when trying to download the document at URL '"
connection_error_message = "Connection Error - Unable to connect to the SQLite database new_log.db"


### Regular Expressions:

### Live Websites:

## News 1 - SBS News: Top Stories

news1_name = 'SBS News'
news1_web_site = 'https://www.sbs.com.au/news/feed'
news1_file = 'sbs_news.xhtml'

# Regular expression to dentify articles, titles, descriptions,
# dates, and pictures:
news1_article_re = 'item>[\s\S]*?<\/item'
news1_title_re = '<title>...CDATA.(.*?)...<\/title>'
news1_desc_re = '<description>...CDATA.(.*?)...<\/description>'
news1_date_re = '<pubDate>(.*?)<\/pubDate>'
news1_pic_re = 'enclosure url="(.*?)" length'


## News 2 - Wired Magazine: Technology News

news2_name = 'Wired Magazine'
news2_web_site = 'https://www.wired.com/feed'
news2_file = 'wired_magazine.xhtml'

# Regular expression to dentify articles, titles, descriptions,
# dates, and pictures:
news2_article_re = 'item>[\s\S]*?<\/item'
news2_title_re = 'title>(.*?)<\/title'
news2_desc_re = 'description>(.*?)<\/description'
news2_date_re = 'pubDate>(.*?)<\/pubDate'
news2_pic_re = 'media:thumbnail url="(.*?)" width'


### Archived Websites:

## News 3 - ABC (USA) International News

news3_name = 'ABC News'
news3_web_site = 'https://abcnews.go.com/abcnews/internationalheadlines'
news3_file = 'abc_news.xhtml'

# Regular expression to dentify articles, titles, descriptions,
# dates, and pictures:
news3_article_re = 'item>[\s\S]*?<\/item'
news3_title_re = '<title>...CDATA. (.*?)...<\/title>'
news3_desc_re = '<description>...CDATA.(.*?)...<\/description'
news3_date_re = 'pubDate>(.*?)<\/pubDate'
news3_pic_re = 'width=\"240.*url=\"(.*?)\".*height=\"456'


## News 4 - Fox News: Technology News

news4_name = 'Fox News'
news4_web_site = 'http://feeds.foxnews.com/foxnews/tech'
news4_file = 'fox_news.xhtml'

# Regular expression to dentify articles, titles, descriptions,
# dates, and pictures:
news4_article_re = 'item>[\s\S]*?<\/item'
news4_title_re = 'title>(.*?)<\/title'
news4_desc_re = 'description>(.*?)<\/description'
news4_date_re = 'pubDate>(.*?)<\/pubDate'
news4_pic_re = 'media:content url="(.*?)" medium="image" isDefault'



#--------------------------------------------------------------------#



##### FUNCTIONS:


#-----------------------------------#


##### SECTION 1:

##### ACCESSING NEWS FEEDS AND RETRIEVING THEIR CONTENT:

##### News feeds are accessed by the functions "download" for live feeds, and "archive_read"
##### for archived feeds, and then are stored in "web_page_contents".
##### These contents are then processed by the "extract_news" function that uses regular expressions
##### to identify news items and their associated data, and store then them in the array "extracterd_news_data".



#### Create a function to read the downloaded archive files:
#    Parameters:
#       * target_filename - a pre-saved news web site file in the program
#                           directory

def archive_read(target_filename = 'download.xhtml'):

    ### Open the file for reading
    ## If unable to open, print an error message to the shell
    try:
        web_page = open(target_filename)
    except FileNotFoundError:
        print(load_error_message + target_filename + "' from the local file\ndirectory\n")
        # Return an emoty web_page_contents list
        return None

    ### Read the contents as a character string
    web_page_contents = web_page.read()
    web_page.close()

    ### Return the document to be used 
    return web_page_contents



#### Create a function that extracts the information from the news website source code using regular expressions:
#    Parameters:
#       *   news_contents    - a news web site complete contents as a character string
#       *   news_identifer   - the regular expression used to identify separate
#                              news items in the news contents
#       *   title_identifier - the regular expression used to identify the title of
#                              each news item
#       *   desc_identifier  - the regular expression used to identify the description
#                              of each news item
#       *   date_identifier  - the regular expression used to identify the date of
#                              each news item
#       *   pic_identifier   - the regular expression used to identify the jpg picture
#                              for each news item

def extract_news(news_contents, news_identifier, title_identifier, desc_identifier, date_identifier, pic_identifier):
    
    ### Create an empty news contents list for storing extrated news items data
    ## Format:  [[title1, desc1, date1, pic1], [title2, desc2, date2, pic2], etc]
    extracted_news_data =[]


    ### Check if the "download" or "archive_read" functions had errors (returned None).
    ## If they did, return an empty "extracted_news_data" list

    if news_contents == None:
        return extracted_news_data 
    

    ### Separate news items from the news contents list using the news identifier
    ### regular expression
    item_list = findall(news_identifier, news_contents)
    
    ### For each news article in the HTML:
    for item in item_list:
        
        ## Extract title using the title regular expression
        title = findall(title_identifier, item)
         
        ## Extract description using the description regular expression
        desc = findall(desc_identifier, item)

        ## Extract publish date using the date regular expression
        date = findall(date_identifier, item)

        ## Extract picture using the jpg regular expression
        pic = findall(pic_identifier, item)

        ## Add the above information for the news article to the news contents list
        extracted_news_data.append([title, desc, date, pic])

    ### Return the list from the function for use
    return extracted_news_data


#-----------------------------------#


##### SECTION 2:

##### WRITING SELECTED FEED STORIES TO THE GUI:

#### This is achieved by the functions "print_news_to_GUI" that writes news data to the GUI
#### and "update_selection" that refreshes the GUI stories.
#### The GUI spin boxes are used to select the number of stories to show for each feed.



#### Create a function that prints the news article summary information to the GUI:
#    Print Format: "tile"[source of the news - date]
#                 Three blank lines then separate this from the next item
#    Parameters:
#       * news_source - the name of the web site where the news item was
#                       downloaded from
#       * news - a nested list of news items and their data in the format
#                [[title1, desc1, date1, pic1],[title2, desc2, date2], etc]
#       * num_articles - the number of news items in news to be printed to the stories list 

def print_news_to_GUI(news_source, news, num_articles):

    ### Check if there are any news items in "news"
    if news == []:
        stories_list.insert(INSERT, 'WARNING: Unable to access ' + news_source + '\n\n')
        return None
    

    ### For all of the news items up to the number of items to be shown on the
    ### stories list, display the item title, source, and date data
    for article in range(int(num_articles)):

        stories_list.insert(INSERT, '\"')
        stories_list.insert(INSERT, str(*news[article][0]))
        stories_list.insert(INSERT, '\"\n[')
        stories_list.insert(INSERT, news_source)
        stories_list.insert(INSERT, ' - ')
        stories_list.insert(INSERT, str(*news[article][2]))
        stories_list.insert(INSERT, ']\n\n\n')


        
#### Create a function that manages writing of story data to the GUI:

## It also manages the status of the "Export Selected" and "Save Selected" buttons
## so that they are only enabed when there are newly selected stories to export/save

def update_selection():
    
    ### Refresh the GUI's stories list
    stories_list.delete(1.0, END)

    
    ### Check if any stories were selected
    ## No stories selected:
    if news1_sb.get() == '0' and news2_sb.get() == '0' and news3_sb.get() == '0' and news4_sb.get() == '0':
        
        # Write instructions to the GUI for the user to select stories
        stories_list.insert(INSERT, 'Please use the above spin boxes to select stories to\ndisplay')

        # Disable the "Export Selected" and "Save Selected" buttons
        export_selected_but.config(text = 'Export Selected', state = DISABLED, bg = 'lightslategrey', fg = 'lightgrey')
        save_selected_but.config(text = 'Save Selected', state = DISABLED, bg = 'lightslategrey', fg = 'lightgrey')

        return None

    ## Stories selected:
    else:
        # Enable the "Export Selected" and "Save Selected" buttons
        export_selected_but.config(text = 'Export Selected', state = NORMAL, bg = 'midnightblue', fg = 'white')
        save_selected_but.config(text = 'Save Selected', state = NORMAL, bg = 'royalblue', fg = 'white')
    
    
    ### Get the required number of news items to display for a website from the
    ### spinbox value. Then write this number of items to the GUI's stories list:

    ## Live Websites:
    if news1_sb.get() != '0':
        number_of_articles = news1_sb.get()
        print_news_to_GUI(news1_name, news1_articles_list, number_of_articles)

    if news2_sb.get() != '0':
        number_of_articles = news2_sb.get()
        print_news_to_GUI(news2_name, news2_articles_list, number_of_articles)

    ## Archived Websites:
    if news3_sb.get() != '0':
        number_of_articles = news3_sb.get()
        print_news_to_GUI(news3_name, news3_articles_list, number_of_articles)

    if news4_sb.get() != '0':
        number_of_articles = news4_sb.get()
        print_news_to_GUI(news4_name, news4_articles_list, number_of_articles)


#-----------------------------------#


##### SECTION 3:
    
##### WRITING EXPORT FILE HTML CODE:
    
##### This is achieved by the functions "write_selected_news_to_html" that writes code for each story, 
##### 'write_news_source_to_html' that writes a list of the news feed names
##### and 'export_html' that manages the generation and saving of the export html document. 



#### Create a function that writes the HTML code for news article summary information for export:
#    Parameters:
#       * news_source - the name of the web site where the news item was
#                       downloaded from
#       * news - a nested list of news items and their data in the format
#                [[title1, desc1, date1, pic1],[title2, desc2, date2], etc]
#       * num_articles - the number of news items in news to be printed to the stories list

def write_selected_news_to_html(news_source, news, num_articles):

    ### For a news item up to the number of items to be shown on the
    ### stories list, display the item title, source, and date data
    
    ### Check if there are any news items in "news"
    ## If None, write a warning message to HTML code   
    if news == []:
        article_data =       ('             <h2>\n',
                              '             ERROR: Was unable to access ', news_source, '\n'
                              '             <br>\n',
                              '             Sorry for the inconvenience\n',
                              '             </h2>\n\n'
                              '            <!-- Seperator -->\n',
                              '            <hr>\n\n')                                
        news_article = string_seperator.join(article_data)        
        return news_article

    ## Otherwise prepare the HTML data for each news item in news
    else:
        ## Create the string "news_article" and append html data to it for each article    
        news_article = ''    
        for article in range(int(num_articles)):
            article_data =       ('                ', news_article, '\n',
                                  '            <item>\n\n',
                                  '                <!-- A news article -->\n\n', 
                                  '                <!-- Article title -->\n',
                                  '                <h2>\n',
                                  '                ', str(*news[article][0]), '\n',
                                  '                </h2>\n\n',
                                  '                <!-- Article image -->\n',
                                  '                <center>\n',
                                  '                <img src ="', str(*news[article][3]), '">\n',
                                  '                </center>\n\n',
                                  '                <!-- Article description -->\n',
                                  '                <p>\n',
                                  '                ', str(*news[article][1]), '\n',
                                  '                </p>\n\n',
                                  '                <!-- Article publish date -->\n',
                                  '                <p style = "font-size: 15px; font-weight: bold; font-style: italic">\n',
                                  '                ', news_source, ' - ', str(*news[article][2]), '\n',
                                  '                </p>\n\n',
                                  '            </item>\n\n\n',
                                  '            <!-- Seperator -->\n',
                                  '            <hr>\n\n')
            news_article = string_seperator.join(article_data)

        return news_article



#### Create a function that writes the HTML code for news sources for export:
#    Parameters:
#       * news_source - the name of the web site where the news item was
#                       downloaded from
#       * news_web_site - URL for the news source


def write_news_source_to_html(news_source, news_web_site):

    source_writeup = ('                <!-- Website link one -->\n',
                      '                <li>\n',
                      '                ', news_source, ': <a href="', news_web_site, '"> ', news_web_site, ' </a>\n',
                      '                </li>\n\n')
    source = string_seperator.join(source_writeup)
    return source



#### Create a function that exports the selected articles as a html file:
## This function is called via the "Export Selection" button in the GUI

def export_html():

    ### Open the html file
    exportfile = open(export_news_file_name, 'w')


    ### Write the header to the export HTML file
    
    ## HTML code for the "head" portion of the html file:    
    head_template = """
    <!DOCTYPE html>
    <html>

        <head>
        
            <!-- Tell the browser to expect Unicode chars -->
            <meta charset = "UHF-8">

            <!-- Title for browser window or tab -->
            <title>
            Blue Box News Archive
            </title>

            <!-- Establish the overall document style -->
            <style type="text/css" xml:space="preserve">
            
                body  {background:#b6cdf0;}
                hr    {width: 85%; border: 2px solid #022459;
                       text-align: center;}
                img   {width: 850px; height: 478px;}
        
                h1    {font-family: "Arial", Arial, sans-serif; font-size: 60px; color:#022459;
                       text-align: center; margin-top: 30px; margin-bottom: 0px;}
                h2    {font-family: "Arial", Arial, sans-serif; font-size: 35px; color:#022459;
                       text-align: center;}
                h3    {font-family: "Arial", Arial, sans-serif; font-size: 30px; color:#022459;
                       text-align: center;}
                   
                p     {font-family: "Arial", Arial, sans-serif; font-size: 20px;
                       text-align: center;}
                li    {font-family: "Arial", Arial, sans-serif; font-size: 20px;
                       margin-left: 400px;}
            
            </style>
            
        </head>
        
        <body>
        
            <!-- Use horizontal lines to seperate the sections of the document -->
            <hr>
            
            <!-- Main heading -->
            <h1 align = "center">
            Blue Box News Archive
            </h1>

            <!-- Splash image - used the logo to keep the theme consistent -->
            <center>
            <img src ="http://icons.iconarchive.com/icons/pelfusion/long-shadow-ios7/128/News-icon.png" style = "width: 200px; height: 200px;">
            </center>

            <!-- Seperator -->
            <hr>
            
"""

    ## Write to HTML:
    exportfile.write(head_template)

    
    ### Write the chosen articles to the html file

    # News 1 - SBS News:
    if news1_sb.get() != '0':
        news1_article_html = write_selected_news_to_html(news1_name, news1_articles_list, news1_sb.get())
        exportfile.write(news1_article_html)

    # News 2 - Wired Magazine:   
    if news2_sb.get() != '0':
        news2_article_html = write_selected_news_to_html(news2_name, news2_articles_list, news2_sb.get())
        exportfile.write(news2_article_html)

    # News 3 - ABC News:
    if news3_sb.get() != '0':
        news3_article_html = write_selected_news_to_html(news3_name, news3_articles_list, news3_sb.get())
        exportfile.write(news3_article_html)

    # News 4 - NBC News:
    if news4_sb.get() != '0':
        news4_article_html = write_selected_news_to_html(news4_name, news4_articles_list, news4_sb.get())
        exportfile.write(news4_article_html)


    ###  Write the websites sources to the html file

    ## Write the "Sources" title to the html document:
    source_title_writeup = ('            <!-- Website sources -->\n',
                            '            <h3>\n',
                            '            ','Sources\n',
                            '            </h3>\n\n',
                            '            <!-- Website links -->\n',
                            '            <ul>\n\n')
    source_title = string_seperator.join(source_title_writeup)
    exportfile.write(source_title)

    ## Write the website links:
    
    # News 1 - SBS News: 
    if news1_sb.get() != '0':
        source1 = write_news_source_to_html(news1_name, news1_web_site)
        exportfile.write(source1)

    # News 2 - Wired Magazine:
    if news2_sb.get() != '0':
        source2 = write_news_source_to_html(news2_name, news2_web_site)
        exportfile.write(source2)

    # News 3 - ABC News: 
    if news3_sb.get() != '0':
        source3 = write_news_source_to_html(news3_name, news3_web_site)
        exportfile.write(source3)

    # News 4 - NBC News:
    if news4_sb.get() != '0':
        source4 = write_news_source_to_html(news4_name, news4_web_site)
        exportfile.write(source4)


    ### Write the "tail" portion of the html file
        
    ## HTML code for the "tail" portion of the html file:    
    tail_template = """
            </ul>

            <!-- Seperator -->
            <hr>
        
        </body>
        
    </html>
    """

    ## Write to HTML:
    exportfile.write(tail_template)


    ### Close the html file:
    exportfile.close()


    ### Change the GUI's "Export Selected" button text to "Selected Exported"
    export_selected_but.config(text = 'Selected Exported', state = DISABLED, bg = 'lightslategray', fg = 'lightgrey')


#-----------------------------------#


##### SECTION 4:

##### SAVE SELECTED NEWS ARTICLE DATA TO A SQLITE3 DATABASE:

##### This is achieved by the functions "write_to_db" which updates the database with each selected news story,
##### and "save_db" which manages the database access and saving of the data to the file



### Create a function that enables us to write to a SQLite Database through Python:
#    Parameters:
#       * connection - the connection from python to SQLite
#       * news_source - the name of the web site where the news item was
#                       downloaded from
#       * news - a nested list of news items and their data in the format
#                [[title1, desc1, date1, pic1],[title2, desc2, date2], etc]
#       * num_articles - the number of news items in news to be printed to the stories list 


def write_to_db(connection, news_source, news, num_articles):

    ### Check for previous errors
    ## If errors have occur, do not save the article data for the file
    if news == []:
        return None
    
    ### Create a cursor object to preform SQLite commands
    cursor = connection.cursor()

    ### Insert data into the table:
    for article in range(int(num_articles)):

        cursor.execute('INSERT INTO selected_stories(headline, news_feed, publication_date) VALUES (?,?,?)', (str(*news[article][0]), news_source, str(*news[article][2])))

        ## Save the changes:
        connection.commit()
    


### Create a function that enables us to save the SQLite Database through Python:
#    Parameters:
#       * db_file - the database file 

def save_db(db_file = 'news_log.db'):

    ### Create a connection to the database
    # If no connection can be established, print an error message to the shell
    db_connection = None
    
    try:
        db_connection = connect(db_file)
        
    except ConnectionError:
        print(connection_error_message + "'\n")
        return

 
    ### Reset the database
    
    ## Create a cursor object to preform SQLite commands:
    cursor = db_connection.cursor()

    ## Clear all row data from the selected_stories table:
    cursor.execute('DELETE FROM selected_stories')
    db_connection.commit()


    ### Add our selected stories from the GUI to the SQLite database
    with db_connection:

        ## Get the required number of news item to display for each website from the tk spin boxes:
        
        # News 1:
        write_to_db(db_connection, news1_name, news1_articles_list, news1_sb.get())

        # News 2:
        write_to_db(db_connection, news2_name, news2_articles_list, news2_sb.get())

        # News 3:
        write_to_db(db_connection, news3_name, news3_articles_list, news3_sb.get())

        # News 4:
        write_to_db(db_connection, news4_name, news4_articles_list, news4_sb.get())

    
    ### Close the database
    db_connection.close()


    ### Change the GUI's "Save Selected" button text to "Selected Saved"
    save_selected_but.config(text = 'Selected Saved', state = DISABLED, bg = 'lightslategray', fg = 'lightgrey')



#--------------------------------------------------------------------#



#####   MAIN PROGRAM CODE:

#### Create archive files
## (this was used to download our archived files and commented out when completed)
#web_download(news3_web_site, news3_file, save_file = True)
#web_download(news4_web_site, news4_file, save_file = True)


#### Import news website content AND Separate news content into a list of articles 

### Live Articles - download from live news feed web sites and remove any end of line characters:
## News 1: 
news1_contents = download(news1_web_site, news1_name, 'xhtml', False)
news1_articles_list = extract_news(news1_contents, news1_article_re, news1_title_re, news1_desc_re, news1_date_re, news1_pic_re)

## News 2:
news2_contents = download(news2_web_site, news2_name, 'xhtml', False)
news2_articles_list = extract_news(news2_contents, news2_article_re, news2_title_re, news2_desc_re, news2_date_re, news2_pic_re)


### Archived Articles - read from the local program directory pre-saved news feed htmls:
## News 3:
news3_contents = archive_read(news3_file)
news3_articles_list = extract_news(news3_contents, news3_article_re, news3_title_re, news3_desc_re, news3_date_re, news3_pic_re)

# News 4:
news4_contents = archive_read(news4_file)
news4_articles_list = extract_news(news4_contents, news4_article_re, news4_title_re, news4_desc_re, news4_date_re, news4_pic_re)



#--------------------------------------------------------------------#



##### BUILDING THE GUI:

### Setup:

## Create a window 
news_aggregator = Tk()

## Give the window a title
news_aggregator.title('Blue Box News Archive')

## Set a size for the window
news_aggregator.geometry("500x630")

## Lock the window size
news_aggregator.resizable(width = False, height = False)


### Formation:

## Create title label for GUI title
title = Label(news_aggregator, text = 'Blue Box News Archive', font = ('Arial', 34), fg = 'midnightblue')
title.grid(row = 1, column = 1, columnspan = 2, padx = 5, pady = 5, sticky = 'n')


## Create "News Feeds" label frame
news_feeds = LabelFrame(news_aggregator, text = 'News Feeds', font = ('Arial', 18), fg = 'lightslategray', padx = 5, pady = 5)
news_feeds.grid(row = 2, column = 1, padx = 40, pady = 5, sticky = 'w')


## Add websites into our label frame

# Live Articles - labels and spinboxes:
news1_lb = Label(news_feeds, text = news1_name + '\n (' + live_date + ')', font = ('Arial', 12), fg = 'forestgreen')
news1_lb.grid(row = 1, column = 1, sticky = 'n')

news1_sb = Spinbox(news_feeds, from_= 0, to = max_articles, width = 2, font = ('Arial', 15))
news1_sb.grid(row = 1, column = 2, padx = 5)

news2_lb = Label(news_feeds, text = news2_name + '\n (' + live_date + ')', font = ('Arial', 12), fg = 'forestgreen')
news2_lb.grid(row = 2, column = 1, sticky = 'n')

news2_sb = Spinbox(news_feeds, from_= 0, to = max_articles, width = 2, font = ('Arial', 15))
news2_sb.grid(row = 2, column = 2, padx = 5)

# Archived Articles - labels and spinboxes:
news3_lb = Label(news_feeds, text = news3_name + '\n (' + archive_download_date + ')', font = ('Arial', 12), fg = 'crimson')
news3_lb.grid(row = 3, column = 1, sticky = 'n')

news3_sb = Spinbox(news_feeds, from_= 0, to = max_articles, width = 2, font = ('Arial', 15))
news3_sb.grid(row = 3, column = 2, padx = 5)

news4_lb = Label(news_feeds, text = news4_name + '\n (' + archive_download_date + ')', font = ('Arial', 12), fg = 'crimson')
news4_lb.grid(row = 4, column = 1, sticky = 'n')

news4_sb = Spinbox(news_feeds, from_= 0, to = max_articles, width = 2, font = ('Arial', 15))
news4_sb.grid(row = 4, column = 2, padx = 5)


## Import the logo from the local program directory
logo_pic = PhotoImage(file = './newspic.png')

logo_label = Label(news_aggregator, image = logo_pic)
logo_label.grid(row = 2, column = 2, sticky = 'w')


## Create label frame to hold selection buttons
options = LabelFrame(news_aggregator, bd = 0)
options.grid(row = 3, column = 1, padx = 2, pady = 3, sticky = 'w')


## Create selection buttons

# Update Selected Button:
update_selected_but = Button(options, text = 'Update News Feed', width = 15, bg = 'midnightblue', fg = 'white', command = update_selection, font = ('Arial', 10))
update_selected_but.grid(row = 1, column = 1, sticky = 'w', padx = 3, pady = 3)

# Export Selected Button:
export_selected_but = Button(options, text = 'Export Selected', width = 15, bg = 'lightslategray', fg = 'lightgrey', state = DISABLED, command = export_html, font = ('Arial', 10))
export_selected_but.grid(row = 1, column = 2, sticky = 'w', padx = 3, pady = 3)

# Save Selected Button:
save_selected_but = Button(options, text = 'Save Selected', width = 32, bg = 'lightslategray', fg = 'lightgrey', state = DISABLED, command = save_db, font = ('Arial', 10))
save_selected_but.grid(row = 2, column = 1, columnspan = 2, sticky = 'w', padx = 3, pady = 3)


## Create "Stories Selected" label frame
stories_selected = LabelFrame(news_aggregator, text = 'Stories Selected', font = ('Arial', 18), fg = 'lightslategray', padx = 5, pady = 5)
stories_selected.grid(row = 5, column = 1, columnspan = 2, padx = 13, pady = 5, sticky = 'w')


## Add text box with scrollbar into our label frame to display our news article summary data

# Scrollbar Creation:
scrollbar = Scrollbar(stories_selected, orient = 'vertical')

# Story Discriptions Textbox Creation:
stories_list = Text(stories_selected, width = 55, height = 13, yscrollcommand = scrollbar.set)
stories_list.insert(INSERT, 'Please select the news feeds you desire via the\nspinboxes, and then press the "Update News Feed" button\nto show your selected stories in this space.\n\nYou will then be able to export your selections into a\nhtml file using the "Export Selected" button.\n\nAnd, then be able to save them into a database using\nthe "Save Selected" button.') 

# Add command to the scrollbar widget:
scrollbar.config(command = stories_list.yview)

# Put these widgets into the label frame:
stories_list.grid(row = 1, column = 1)
scrollbar.grid(row = 1, column = 2, sticky = N + S + W)



## Start the event loop
news_aggregator.mainloop()









