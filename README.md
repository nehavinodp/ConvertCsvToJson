**Redirect CSV to JSON Converter**

A lightweight C# console app that converts a CSV file of redirects into a JSON format suitable for Vercel Edge Config.
Built as part of my blog on migrating redirects from Sitecore to Edge Config.

**CSV Format**

Your CSV should contain these three columns: `source,destination,permanent`  
Example:
```bash
/old-page,/new-page,true  
/legacy-page,/resources,false
```

**Output JSON**

The app generates JSON in this structure:

```bash
{
  "/old-page": { "destination": "/new-page", "permanent": true }
}
```

**Usage**

	1.	Open Program.cs
	2.	Update the CSV input file path and JSON output file path
	3.	Run the application.
This creates your JSON file at the output path you configured.



