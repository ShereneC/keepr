<template>
  <div class="modal" :id="'keep-modal' + keep.id" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg modal-dialog-centered" role="document">
      <div class="modal-content container-fluid">
        <div class="modal-header row">
          <!-- Column for the image -->
          <div class="col-md-6">
            <img class="" :src="keep.img" alt="Keep image" style="width:100%">
          </div>
          <!-- Column for all other info -->
          <div class="col-md-6 d-flex flex-column">
            <div class="row">
              <div class="col d-flex justify-content-around">
                <h6><span class="fa fa-eye">  </span>  {{ keep.view }}</h6>
                <h6 class="p-0 m-0">
                  <span><i class="material-icons">attachment</i></span>  {{ keep.keeps }}
                </h6>
                <h6><span class="fa fa-share-alt">  </span>{{ keep.shares }}</h6>
              </div>
            </div>
            <div class="row">
              <div class="col pt-3 pb-1">
                <h2>{{ keep.name }}</h2>
              </div>
            </div>
            <!-- This row is for the dsecription only -->
            <div class="row">
              <!-- Stretch never works!!! Why won't it stretch down?   align-self-stretch -->
              <div class="col d-flex mb-auto">
                <h6 class="text-left">
                  {{ keep.description }}
                </h6>
              </div>
            </div>
            <!-- Added this row just for a line -->
            <div class="row border-bottom p-1">
            </div>
            <!-- Last row of info section - should be all the way down at the bottom -->
            <div class="row">
              <div class="col d-flex">
                <h5>Add To Vault +</h5>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  <span class="fa fa-trash"></span>
                </button>
                <div class="div">
                  <img :src="keep.creator.picture" alt="profile image" class="profile-pic mt-auto ml-auto">
                  {{ keep.creator.name }}
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- <h5 class="modal-title">
            {{ keep.name }} Details
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <p>Modal body text goes here.</p>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary">
            Save changes
          </button>
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
        </div>
      </div>
    </div>
  </div> -->
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import $ from 'jquery'

export default {
  props: {
    keep: { type: Object, required: true }
  },
  name: 'KeepModal',
  setup() {
    return {
      // keep: computed(() => AppState.activeKeep) - At this point in time, I am not using activeKeep, do I need to?  Not seeing where Mark used it in restaurants.
    }
  },
  components: {}
}

</script>

<style lang="scss" scoped>
.line {
  border: 10px #000;
  width: 80%;
  margin: auto;
  margin-top: 5%;
  margin-bottom: 5%;
  }

  .box {
  flex-grow: 1;
  border: 3px solid rgba(0,0,0,.2);
}
.profile-pic{
  height: 3rem;
  // width: 5vw;
  border-radius: 50%
  // object-fit: cover;
}
</style>
