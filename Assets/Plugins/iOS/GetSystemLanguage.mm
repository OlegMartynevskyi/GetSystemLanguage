/*
 * This Plug in will return the system language code.
 */
extern "C"
{
	char* cStringCopy(const char* string)
	{
    	if (string == NULL)
        	return NULL;
    	char* res = (char*)malloc(strlen(string) + 1);
    	strcpy(res, string);
    	return res;
	}

	char* _getSystemLanguage()
	{
		NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
		NSArray *languages = [defaults objectForKey:@"AppleLanguages"];
    	NSString *currentLanguage = [languages objectAtIndex:0];    
    	return cStringCopy([currentLanguage UTF8String]);
	}	
}