<template>
  <div class="about text-center container">
    <h1>Welcome {{ account.name }}</h1>
    <router-link class="mt-auto ml-auto" :to="{name: 'ProfilePage', params: {id: account.id }}">
      <img :src="account.picture" alt="profile image" class="profile-pic mt-auto ml-auto">
    </router-link>
    <p>{{ account.email }}</p>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Account',
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
