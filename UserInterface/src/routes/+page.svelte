<script>
	import { onMount } from 'svelte';

	function callDotNet() {
		window.external.sendMessage('Hi .NET! 🤖');
	}

	onMount(() => {
		window.external.receiveMessage((message) => alert(message));
	});
</script>

<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://kit.svelte.dev">kit.svelte.dev</a> to read the documentation aaaaaaa</p>
<button on:click={callDotNet}>Asd</button>
