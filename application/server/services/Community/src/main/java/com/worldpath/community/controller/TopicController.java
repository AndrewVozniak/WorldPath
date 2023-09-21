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
     * @Description Get topic by ID
     * @param topicID Topic ID
     * @return TopicDTO
     */
    @GetMapping("/{topicID}")
    public ResponseEntity<TopicDTO> getTopicById(@PathVariable String topicID) {
        TopicDTO topic = topicService.getTopicById(topicID);

        if (topic == null) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(topic);
    }

    /**
     * @Description Create topic
     * @param topicDTO TopicDTO
     * @param userId User ID
     * @return TopicDTO
     */
    @PostMapping("/")
    public ResponseEntity<TopicDTO> createTopic(@RequestBody TopicDTO topicDTO,
                                                @RequestHeader("Userid") String userId) {
        topicDTO.setUser_id(userId);

        return ResponseEntity.ok(topicService.createTopic(topicDTO));
    }

    /**
     * @Description Update topic
     * @param topicDTO TopicDTO
     * @param topicID Topic ID
     * @return TopicDTO
     */
    @PutMapping("/{topicID}")
    public ResponseEntity<TopicDTO> updateTopic(@RequestBody TopicDTO topicDTO,
                                                @RequestHeader("Userid") String userId,
                                                @PathVariable String topicID) {
        topicDTO.setUser_id(userId);

        TopicDTO topic = topicService.updateTopicById(topicDTO, topicID);

        if (topic == null) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(topic);
    }

    /**
     * ! Description: Get all topics
     * @return List of TopicDTO
     */
    @GetMapping("/")
    public ResponseEntity<Iterable<TopicDTO>> getAllTopics() {
        return ResponseEntity.ok(topicService.getAllTopics());
    }
}
