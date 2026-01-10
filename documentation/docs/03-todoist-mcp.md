# Todoist MCP

Integrate AI assistants with Todoist using the [Model Context Protocol](https://modelcontextprotocol.io/) (MCP), an open standard for secure access to your tasks and projects. Our hosted MCP server works with Claude, ChatGPT, Cursor, and VS Code.

- **Easy setup:** OAuth in a minute.
- **Full access:** Read, create, and update your tasks & projects.
- **Use cases:** Daily reviews, project planning, natural-language queries.

## Setup guide

**Primary URL (Streamable HTTP):**

`https://ai.todoist.net/mcp`

### Claude

1. Open **Settings → Connectors → Add custom connector**.
2. Enter `https://ai.todoist.net/mcp` and complete OAuth.

### Cursor

Create `~/.cursor/mcp.json` (global) or `.cursor/mcp.json` (project):

```json
{
  "mcpServers": {
    "todoist": {
      "command": "npx",
      "args": ["-y", "mcp-remote", "https://ai.todoist.net/mcp"]
    }
  }
}
```

Then enable the server in Cursor settings if prompted.

### Claude Code (CLI)

```bash
claude mcp add --transport http todoist https://ai.todoist.net/mcp
```

### Visual Studio Code

Command Palette → MCP: Add Server → Type HTTP and use:

```json
{
  "servers": {
    "todoist": {
      "type": "http",
      "url": "https://ai.todoist.net/mcp"
    }
  }
}
```

### Other Clients

`npx -y mcp-remote https://ai.todoist.net/mcp`
