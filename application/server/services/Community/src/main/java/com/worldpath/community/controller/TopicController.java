package com.worldpath.community.controller;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.worldpath.community.DTO.TopicDTO;

import com.worldpath.community.service.TopicService;

@RestController
@RequestMapping("/topic")
public class TopicController {
    private final TopicService topicService;

    public TopicController(TopicService TopicService) {
        this.topicService = TopicService;
    }

    /**
     * Description: Get topic by ID
     * @param topicID Topic ID
     * @return TopicDTO
     */
    @GetMapping("/{topicID}")
    public ResponseEntity<TopicDTO> getTopicById(@PathVariable String topicID) {
        return ResponseEntity.ok(topicService.getTopicById(topicID));
    }

    /**
     * Description: Create topic
     * @param topicDTO TopicDTO
     * @param userID User ID
     * @return TopicDTO
     */
    @PostMapping("/")
    public ResponseEntity<TopicDTO> createTopic(@RequestBody TopicDTO topicDTO, @RequestHeader("Userid") String userID) {
        topicDTO.setUser_id(userID);

        return ResponseEntity.ok(topicService.createTopic(topicDTO));
    }
}
